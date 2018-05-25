using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FireInit : MonoBehaviour {
    public DatabaseReference reference;
    List<Item> list = new List<Item>();
    public GameObject content_pre;
    public Transform parent_trans;
    void init()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("");
        //FirebaseApp.DefaultInstance.SetEditorP12FileName("");
        //FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("");
        //FirebaseApp.DefaultInstance.SetEditorP12Password("");


        // Get the root reference location fo Database
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        

    }
	// Use this for initialization
	void Start () {
        init();
        writeScore("Test", 2222);
        getRank();
    }
	
	// Update is called once per frame
	void Update () {
		
	}



public void getRank()
{
    FirebaseDatabase.DefaultInstance
        .GetReference("Rank").OrderByChild("score").LimitToLast(10).GetValueAsync().ContinueWith(task =>
    {
        if (task.IsFaulted)
        {
            Debug.Log("failed");
        }
        else if (task.IsCompleted)
        {
            foreach (DataSnapshot childSnapshot in task.Result.Children)
            {
                list.Add(new Item(childSnapshot.Child("username").Value.ToString(), int.Parse(childSnapshot.Child("score").Value.ToString()), childSnapshot.Child("wdt").Value.ToString()));
                //Debug.Log(childSnapshot.Child("username").Value.ToString()+childSnapshot.Child("score").Value.ToString());
            }
            display();
        }
    });
    }

    public void display()
    {



        Debug.Log(list.Count);
        for (int i = list.Count - 1; i >= 0; i--)
        {
            Debug.Log("User : " + list[i].username + "Score : " + list[i].score);
        }
    }

    public void writeScore(string username, int score)
    {
        Item item = new Item(username, score, DateTime.Now.ToString());
        string json = JsonUtility.ToJson(item);
        reference.Child("Rank").Child(username).SetRawJsonValueAsync(json);
    }

}
public class Item
{
    public string username;
    public int score;
    public string wdt;
    public Item(string username, int score, string wdt)
    {
        this.username = username;
        this.score = score;
        this.wdt = wdt;
    }

}
