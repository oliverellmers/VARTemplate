/* 
*   NatCorder
*   Copyright (c) 2019 Yusuf Olokoba
*/

namespace NatCorder.Examples {

    #if UNITY_EDITOR
	using UnityEditor;
	#endif
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;
    using Clocks;
    using Inputs;
    

    public class ReplayCam : MonoBehaviour {

        /**
        * ReplayCam Example
        * -----------------
        * This example records the screen using a `CameraRecorder`.
        * When we want mic audio, we play the mic to an AudioSource and record the audio source using an `AudioRecorder`
        * -----------------
        * Note that UI canvases in Overlay mode cannot be recorded, so we use a different mode (this is a Unity issue)
        */

        private ScreenRecorderUIController screenRecorderUIController;

        //[Header("Recording")]
        //public int videoWidth = Screen.width;
        //public int videoHeight = Screen.height;
        private int videoWidth = Screen.width;
        private int videoHeight = Screen.height;

        [Header("Microphone")]
        public bool recordMicrophone;
        public AudioSource microphoneSource;

        [HideInInspector]
        public string saveFolder = "";
        [HideInInspector]
        public string videoPath = "";

        private Texture2D ss;
        private MP4Recorder videoRecorder;
        private IClock recordingClock;
        private CameraInput cameraInput;
        private AudioInput audioInput;
        private bool isVideo = false; //determine whether it is a video or image to be shared

        

        public void StartRecording () {

            screenRecorderUIController = FindObjectOfType<ScreenRecorderUIController>();
            if (System.IO.File.Exists(videoPath)) {
                System.IO.File.Delete(videoPath);
            }
            

            // Start recording
            recordingClock = new RealtimeClock();
            videoRecorder = new MP4Recorder(
                videoWidth,
                videoHeight,
                30,
                recordMicrophone ? AudioSettings.outputSampleRate : 0,
                recordMicrophone ? (int)AudioSettings.speakerMode : 0,
                OnReplay
            );
            // Create recording inputs
            cameraInput = new CameraInput(videoRecorder, recordingClock, Camera.main);
            if (recordMicrophone) {
                StartMicrophone();
                audioInput = new AudioInput(videoRecorder, recordingClock, microphoneSource, true);
            }
        }

        public void SaveScreenshot() {
            screenRecorderUIController = FindObjectOfType<ScreenRecorderUIController>();
            StartCoroutine(TakeScreenshotAndSave());
        }

        private IEnumerator TakeScreenshotAndSave()
        {
            yield return new WaitForEndOfFrame();

            GameObject.Find("ScreenRecorderCanvas(Clone)").GetComponent<Canvas>().enabled = false;

            ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            ss.Apply();

            // Save the screenshot to Gallery/Photos
            Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(ss, saveFolder, "My img {0}.png"));

            isVideo = false;
            screenRecorderUIController.ShareDialogueVisibility(true, 0.25f);
            screenRecorderUIController.SetMessageText("Screenshot saved to " + saveFolder +  " album");
            // To avoid memory leaks
            //Destroy(ss);

            GameObject.Find("ScreenRecorderCanvas(Clone)").GetComponent<Canvas>().enabled = true;
        }

        private void StartMicrophone () {
            #if !UNITY_WEBGL || UNITY_EDITOR // No `Microphone` API on WebGL :(
            // Create a microphone clip
            microphoneSource.clip = Microphone.Start(null, true, 60, 48000);
            while (Microphone.GetPosition(null) <= 0) ;
            // Play through audio source
            microphoneSource.timeSamples = Microphone.GetPosition(null);
            microphoneSource.loop = true;
            microphoneSource.Play();
            #endif
        }

        public void StopRecording () {
            // Stop the recording inputs
            if (recordMicrophone) {
                StopMicrophone();
                audioInput.Dispose();
            }
            cameraInput.Dispose();
            // Stop recording
            videoRecorder.Dispose();
        }

        private void StopMicrophone () {
            #if !UNITY_WEBGL || UNITY_EDITOR
            Microphone.End(null);
            microphoneSource.Stop();
            #endif
        }

        private void OnReplay (string path) {
            Debug.Log("Saved recording to: "+path);

            screenRecorderUIController.SetMessageText("Screen recording saved to " + saveFolder + " album");

#if UNITY_EDITOR
            EditorUtility.OpenWithDefaultApp(path);
#elif UNITY_IOS
            Handheld.PlayFullScreenMovie("file://" + path, Color.black, FullScreenMovieControlMode.CancelOnInput, FullScreenMovieScalingMode.AspectFit);
#elif UNITY_ANDROID
            Handheld.PlayFullScreenMovie(path, Color.black, FullScreenMovieControlMode.CancelOnInput, FullScreenMovieScalingMode.AspectFit);
#endif
            videoPath = path;

            isVideo = true;
            screenRecorderUIController.ShareDialogueVisibility(true, 0.25f);
            NativeGallery.SaveVideoToGallery(path, saveFolder, "recording_" + System.IO.Path.GetFileName(path), null);
        }

        public void ShareImage() {
            if (isVideo)
            {
                NatShareU.NatShare.Share(videoPath);
                videoPath = null;
            }
            else {
                NatShareU.NatShare.Share(ss);
                Destroy(ss);
            }
            
        }

        public void DiscardVideo() {
            if (isVideo)
            {
                System.IO.File.Delete(videoPath);
                videoPath = null;
            }
            else {
                Destroy(ss);
            }
        }
    }
}