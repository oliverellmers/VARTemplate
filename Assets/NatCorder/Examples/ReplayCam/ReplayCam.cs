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

        //[Header("Recording")]
        //public int videoWidth = Screen.width;
        //public int videoHeight = Screen.height;
        private int videoWidth = Screen.width;
        private int videoHeight = Screen.height;

        [Header("Microphone")]
        public bool recordMicrophone;
        public AudioSource microphoneSource;

        private MP4Recorder videoRecorder;
        private IClock recordingClock;
        private CameraInput cameraInput;
        private AudioInput audioInput;

        public void StartRecording () {            
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


            //TODO:

            //1. store the path of this video in a variable

            //2. replay the video

            // Playback the video
#if UNITY_EDITOR
            EditorUtility.OpenWithDefaultApp(path);
#elif UNITY_IOS
            Handheld.PlayFullScreenMovie("file://" + path);
#elif UNITY_ANDROID
            Handheld.PlayFullScreenMovie(path);
#endif

            //Show icon to indicate replaying the video

            //Show dialogue with a share icon and 'tap to share' button

            //replaying/looping video behind slightly blurred out

            // X button to hide 'tap to share' dialogue

            //If dialogue clossed, replay video un-blurred

            //small share icon button visible

            System.IO.File.Move(path, Application.persistentDataPath );

            //NatShareU.NatShare.SaveToCameraRoll(path);

            //Do all of this in a while loop:

            //Tick button to save to camera roll

            //X button to discard - deletes

            //if share video is true{
            //NatShareU.NatShare.Share(path);
            //}


            // Remove video from original path to save space

            //System.IO.File.Delete(path);


            
        }
    }
}