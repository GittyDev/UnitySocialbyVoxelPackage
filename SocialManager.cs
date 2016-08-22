using UnityEngine;
using System.Collections;
using VoxelBusters.NativePlugins;

public class SocialManager : MonoBehaviour {
	
	[HideInInspector]
	public string FACEBOOK_LINK = "https://facebook.com/";

	[HideInInspector]
	public string TWITTER_LINK = "https://twitter.com/";

	public string mFaceBookLink;
	public string mTwitterLink;

	public GameObject dlg_errorFB;
	public GameObject dlg_errorTwitt;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnFaceBookLink(){
		//Application.OpenURL (FACEBOOK_LINK + mFaceBookLink);
		if (NPBinding.Sharing.IsFBShareServiceAvailable ()) {
			// Create share sheet
			FBShareComposer _shareSheet = new FBShareComposer ();    
			_shareSheet.AttachScreenShot ();

			NPBinding.UI.SetPopoverPointAtLastTouchPosition (); // To show popover at last touch point on iOS. On Android, its ignored.
			NPBinding.Sharing.ShowView (_shareSheet, FinishedSharing);
		} else {
			OnOpenErrorFBMsg();
		}
	}

	public void OnAppLink(){
		//Application.OpenURL (FACEBOOK_LINK + mFaceBookLink);
		// Create share sheet
		//OnCloseMsg();
		SocialShareSheet _shareSheet     = new SocialShareSheet();    
		_shareSheet.AttachScreenShot();

		NPBinding.UI.SetPopoverPointAtLastTouchPosition(); // To show popover at last touch point on iOS. On Android, its ignored.
		NPBinding.Sharing.ShowView(_shareSheet, FinishedSharing);
	}

	public void OnTwiterLink(){
		//Application.OpenURL (TWITTER_LINK + mTwitterLink);
		if (NPBinding.Sharing.IsTwitterShareServiceAvailable ()) {
			// Create composer
			TwitterShareComposer _composer    = new TwitterShareComposer();
			_composer.AttachScreenShot();

			// Show share view
			NPBinding.Sharing.ShowView(_composer, FinishedSharing);

		} else {
			//show Error
			OnOpenErrorTwittMsg();
		}
	}

	public void OnOpenErrorFBMsg(){
		dlg_errorFB.SetActive(true);
		dlg_errorFB.GetComponent<Animator> ().SetBool ("Show", true);
	}

	public void OnCloseFBMsg(){
		dlg_errorFB.GetComponent<Animator> ().SetBool ("Show", false);
		dlg_errorFB.SetActive (false);
	}

	public void OnOpenErrorTwittMsg(){
		dlg_errorTwitt.SetActive(true);
		dlg_errorTwitt.GetComponent<Animator> ().SetBool ("Show", true);
	}

	public void OnCloseTwittMsg(){
		dlg_errorTwitt.GetComponent<Animator> ().SetBool ("Show", false);
		dlg_errorTwitt.SetActive (false);
	}

	void FinishedSharing (eShareResult _result)
	{
		Debug.Log("Finished sharing");
		Debug.Log("Share Result = " + _result);
	}
}
