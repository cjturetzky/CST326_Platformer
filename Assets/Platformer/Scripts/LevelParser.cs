using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    public string filename;
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public GameObject spikePrefab;
    public GameObject goalPrefab;
    public Transform environmentRoot;

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel();
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }

        // Go through the rows from bottom to top
        int row = 0;
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            int column = 0;
            char[] letters = currentLine.ToCharArray();
            foreach (var letter in letters)
            {
                // Todo - Instantiate a new GameObject that matches the type specified by letter
                GameObject newObject;
                if(letter == 'x'){
                    newObject = Instantiate(rockPrefab);
                    // Todo - Position the new GameObject at the appropriate location by using row and column
                    newObject.transform.position = new Vector3(column + 0.5f, row + 0.5f, 0f);
                    // Todo - Parent the new GameObject under levelRoot
                    newObject.transform.SetParent(environmentRoot);
                }
                else if(letter == 'b'){
                    newObject = Instantiate(brickPrefab);
                    newObject.transform.position = new Vector3(column + 0.5f, row + 0.5f, 0f);
                    newObject.transform.SetParent(environmentRoot);
                }
                else if(letter == '?'){
                    newObject = Instantiate(questionBoxPrefab);
                    newObject.transform.position = new Vector3(column + 0.5f, row + 0.5f, 0f);
                    newObject.transform.SetParent(environmentRoot);
                }
                else if(letter == 's'){
                    newObject = Instantiate(stonePrefab);
                    newObject.transform.position = new Vector3(column + 0.5f, row + 0.5f, 0f);
                    newObject.transform.SetParent(environmentRoot);
                }
                else if(letter == 'g'){
                    newObject = Instantiate(goalPrefab);
                    newObject.transform.position = new Vector3(column + 0.5f, row + 0.5f, 0f);
                    newObject.transform.SetParent(environmentRoot);
                }
                else if(letter == '^'){
                    newObject = Instantiate(spikePrefab);
                    newObject.transform.position = new Vector3(column + 0.5f, row + 0.5f, 0f);
                }
                
                column++;
            }
            row++;
        }
    }

    // --------------------------------------------------------------------------
    private void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }
}
