using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;


public class FileLoader : MonoBehaviour
{

    public string fileName;
    public string url;
    
    public Texture2D Source_texture;
    public string Source_video;

    public FileLoader(string fileName, string url){
        this.fileName = fileName;
        this.url = url;
    }

    public IEnumerator LoadImage() {
        
        string filePath =  Application.streamingAssetsPath+"/"+fileName;
        Texture2D myTexture = null;
        byte[] fileData;
 
        //Check if file image is available in local storage
        if ( File.Exists(filePath) )
        {
            fileData = File.ReadAllBytes(filePath);
            myTexture = new Texture2D(2, 2);
            myTexture.LoadImage(fileData);
        }else{

            //Download file image from external url
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError) {
            
            }
            else {

                myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
               

                //save in storage
                File.WriteAllBytes(Application.streamingAssetsPath+"/"+fileName, myTexture.EncodeToPNG());
            }
        }

        Source_texture = myTexture;
        yield return null;
    }


    public IEnumerator LoadVideo() {

        
        //load in storage
        string filePath =  Application.streamingAssetsPath+"/"+fileName+".mp4";

        if ( File.Exists(filePath) )
        {
           
            Source_video = filePath;
        }else{
            UnityWebRequest _videoRequest = UnityWebRequest.Get (url);

            yield return _videoRequest.SendWebRequest();

            if (_videoRequest.isDone == false || _videoRequest.error != null)
            {   Debug.Log ("Request = " + _videoRequest.error );}


            byte[] _videoBytes = _videoRequest.downloadHandler.data;

    
            File.WriteAllBytes (filePath, _videoBytes);
            Debug.Log (filePath);

            Source_video = filePath;
            yield return null;
        }
    }
}
