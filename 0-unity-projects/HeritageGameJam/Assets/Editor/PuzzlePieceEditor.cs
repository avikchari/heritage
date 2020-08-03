using UnityEditor;              //Inherit Editor functionality
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System;                   //Catch nullreference exceptions

//inherit from editor
public class PuzzleBoardEditor : EditorWindow
{
    private UnityEngine.Object selectedPuzzleMaster;     //The master gameobject that checks for puzzle success

    //Textures
    public Texture simpleBox;
    public Texture2D puzzleMask;
    private Texture2D invertedpuzzleMask;
    public Texture2D puzzleImage;

    //Puzzle Board Inventory
    int pb_currentGrid = 00;           //tenth = row, ones = col
    int pb_nextGrid = 00;
    Vector2 pb_GridDimension = new Vector2(3, 3);
    Vector2 pb_NextGridDimension = new Vector2(3, 3);
    GUIContent[] pb_GUIContent;
    bool pb_GridTooLarge = false;
    int pb_existingPuzzlePieces = 0;
    Vector2 savedCorrectLocation = Vector2.zero;


    //Tools
    bool tools_noMoreInventory = false;
    private Texture2D assignPuzzleImage;
    private Texture2D assignPuzzleMask;

    //Drag and Drop
    bool startDrag = false;
    bool selectedPuzzle = false;
    Vector2 draggableTopLeft = Vector2.zero;           //basically the puzzle inventor zone
    Vector2 draggableBotRight = Vector2.zero;
    Vector2 droppableTopLeft = Vector2.zero;           //basically the puzzle solution zone
    Vector2 droppableBotRight = Vector2.zero;
    Vector2 currentPieceLocation = Vector2.zero;

    [MenuItem("CustomTools/PuzzleBoardEditor")]
    public static void Open_PuzzlePieceEditor()
    {
        PuzzleBoardEditor window = (PuzzleBoardEditor)EditorWindow.GetWindow(typeof(PuzzleBoardEditor));
        window.minSize = new Vector2(1000, 720);

        window.Show();
    }

    public void Awake()
    {
        //Initalise Defaults

        //Puzzle Grid Board
        pb_currentGrid = 0;         //top left grid
        pb_GridDimension = new Vector2(3, 3);         //3x3 default
        pb_GUIContent = new GUIContent[9];
        for (int i = 0; i < 9; ++i)
        {
            string toolTip = "No Puzzle Piece Data";
            pb_GUIContent[i] = new GUIContent(simpleBox, toolTip);
        }
        pb_existingPuzzlePieces = 0;

        //Puzzle board tools
        InvertMaskTexture();
    }

    public void MouseUpdate()
    {
        if (!startDrag)
        {
            //Debug.Log(Event.current.type == EventType.MouseDown);
            if (Event.current.type == EventType.MouseDrag)
            {
                Debug.Log(Event.current.mousePosition.y);
                if (Event.current.mousePosition.x > draggableTopLeft.x && Event.current.mousePosition.x < draggableBotRight.x
                    && Event.current.mousePosition.y > draggableTopLeft.y && Event.current.mousePosition.y < draggableBotRight.y)
                {
                    Debug.Log("ADSSASD");
                    startDrag = true;

                }
            }
        }
        else
        {
            if (Event.current.type == EventType.MouseUp && Event.current.button == 0 && selectedPuzzle)
            {
                Debug.Log("ADSSASD");
                //aabb check
                if (Event.current.mousePosition.x > droppableTopLeft.x && Event.current.mousePosition.x < droppableBotRight.x
                    && Event.current.mousePosition.y < droppableTopLeft.y && Event.current.mousePosition.y > droppableBotRight.y)
                {
                    Debug.Log("ADSSASD");
                }

                startDrag = false;
            }
            selectedPuzzle = false;
        }
    }

    public void OnGUI()
    {
        Rect lastRect = new Rect();      //get the last drawn position
        string latestMessageLog = "working fine";

        GUILayout.Label("Features:\nCustomise Graphics for Puzzles and Create Puzzle Solutions");

        GUILayout.BeginHorizontal();
        // Space to view graphic appearance puzzle board
        GUILayout.BeginVertical(GUIStyle.none, GUILayout.Width(500), GUILayout.Height(128));
        GUILayout.Label("Puzzle Pieces Available", EditorStyles.boldLabel);
        // Show Puzzle Pieces available
        EditorGUI.BeginDisabledGroup(false);        //disable when no master object selected
        EditorGUI.BeginChangeCheck();
        pb_NextGridDimension = EditorGUILayout.Vector2Field("Dimension", pb_GridDimension, GUILayout.MaxWidth(300));
        GUILayout.BeginHorizontal();
        if (EditorGUI.EndChangeCheck()) Update_PuzzleDimensions();
        if (pb_GridTooLarge) latestMessageLog = "Too Many Puzzle Pieces!";

        // Click on which Puzzle Pieces to edit
        EditorGUI.BeginChangeCheck();
        pb_nextGrid = GUILayout.SelectionGrid(pb_currentGrid, pb_GUIContent, (int)pb_GridDimension.x, GUILayout.MaxWidth(300), GUILayout.MaxHeight(300));
        if (EditorGUI.EndChangeCheck()) OnPuzzlePiece_Select();

        lastRect = GUILayoutUtility.GetLastRect();
        draggableTopLeft = new Vector2(lastRect.x, lastRect.y);
        draggableBotRight = new Vector2(lastRect.x + 300, lastRect.y - 300);
        EditorGUI.PrefixLabel(new Rect(lastRect.x + 320, lastRect.y - 20, 100, 100), 0, new GUIContent("Selected Puzzle Piece"));
        GUI.DrawTexture(new Rect(lastRect.x + 350, lastRect.y, 112, 112), puzzleImage);
        GUI.DrawTexture(new Rect(lastRect.x + 350, lastRect.y, 112, 112), invertedpuzzleMask);
        GUILayout.EndHorizontal();
        GUILayout.Space(16);

        GUILayout.Label("Puzzle Pieces Tools", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUI.enabled = !tools_noMoreInventory;
        if (GUILayout.Button("Make New Puzzle Piece", GUILayout.MaxWidth(256))) MakeNewPuzzlePiece();
        if (GUILayout.Button("Add Piece To Board", GUILayout.MaxWidth(256))) AddPieceToBoard();
        GUILayout.EndHorizontal();
        GUI.enabled = true;

        GUILayout.BeginHorizontal();
        assignPuzzleImage = (Texture2D)EditorGUILayout.ObjectField(assignPuzzleImage, typeof(Texture2D), false);
        if (GUILayout.Button("Update Puzzle Image", GUILayout.MaxWidth(256))) UpdatePuzzleImage();
        assignPuzzleMask = (Texture2D)EditorGUILayout.ObjectField(assignPuzzleMask, typeof(Texture2D), false);
        if (GUILayout.Button("Update Puzzle Mask", GUILayout.MaxWidth(256))) UpdatePuzzleMask();
        if (tools_noMoreInventory)
        {
            latestMessageLog = "Unable to make new piece - No more empty slot";
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("ActionLogs");
        GUILayout.TextField(latestMessageLog, GUILayout.MaxHeight(20));
        GUILayout.Space(16);

        GUILayout.Label("Puzzle Solution", EditorStyles.boldLabel);
        lastRect = GUILayoutUtility.GetLastRect();
        GUI.DrawTexture(new Rect(lastRect.x + 16, lastRect.y + 16, 500, 300), simpleBox);
        droppableTopLeft = new Vector2(lastRect.x + 16, lastRect.y + 16);
        droppableBotRight = new Vector2(lastRect.x + 516, lastRect.y + 316);

        GUILayout.Space(300);
        GUILayout.BeginHorizontal();
        Vector2 mousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        EditorGUILayout.Vector2Field("Current Piece Location", currentPieceLocation, GUILayout.MaxWidth(240));
        EditorGUILayout.Vector2Field("Saved Piece Location", savedCorrectLocation, GUILayout.MaxWidth(240));
        GUILayout.EndHorizontal();

        EditorGUI.EndDisabledGroup();
        GUILayout.EndVertical();

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        GUILayout.Space(10);
        //Settings 
        GUILayout.BeginVertical();
        GUILayout.Label("Target Puzzle Master Object", EditorStyles.boldLabel);
        selectedPuzzleMaster = EditorGUILayout.ObjectField(selectedPuzzleMaster, typeof(GameObject), true);

        //if (Check_ValidPuzzleObj(selectedPuzzle))
        //{
        //    GUILayout.Label("Attributes", EditorStyles.boldLabel);


        //}
        //else
        //{
        //    GUILayout.Label("Non-puzzle Piece Gameobject selected");
        //}
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
        MouseUpdate();
    }

    void Update_PuzzleDimensions()
    {
        //round float to int
        pb_NextGridDimension.x = Mathf.FloorToInt(pb_NextGridDimension.x);
        pb_NextGridDimension.y = Mathf.FloorToInt(pb_NextGridDimension.y);

        //Check if grid is too large
        if (pb_NextGridDimension.x > 10 || pb_NextGridDimension.y > 10)
        {
            pb_GridTooLarge = true;
            return;
        }
        tools_noMoreInventory = false;
        pb_GridDimension = pb_NextGridDimension;
        pb_GridTooLarge = false;

        GUIContent[] oldGUIContent = pb_GUIContent;
        pb_GUIContent = new GUIContent[(int)(pb_GridDimension.x * pb_GridDimension.y)];         //3x3 default

        //copy over old data into new
        int i = 0;
        for (; i < oldGUIContent.Length && i < pb_GUIContent.Length; ++i)
        {
            pb_GUIContent[i] = oldGUIContent[i];
        }
        for (; i < pb_GUIContent.Length; ++i)
        {
            string toolTip = "grid number = " + i.ToString();
            pb_GUIContent[i] = new GUIContent(simpleBox, toolTip);
        }
    }


    void OnPuzzlePiece_Select()
    {
        //if(pb_SwapEnabled)
        //{
        //    //3 way swap
        //    GUIContent temp = pb_GUIContent[pb_currentGrid];
        //    pb_GUIContent[pb_currentGrid].image = pb_GUIContent[pb_nextGrid].image;
        //    pb_GUIContent[pb_nextGrid].image = temp.image;
        //}
        pb_currentGrid = pb_nextGrid;
        InvertMaskTexture();
        selectedPuzzle = true;
    }

    void InvertMaskTexture()
    {
        if (invertedpuzzleMask) DestroyImmediate(invertedpuzzleMask);
        invertedpuzzleMask = new Texture2D(puzzleMask.width, puzzleMask.height, puzzleMask.format, (puzzleMask.mipmapCount != 0));
        for (int m = 0; m < puzzleMask.mipmapCount; ++m)
        {
            invertedpuzzleMask.SetPixels(puzzleMask.GetPixels(m), m);
        }

        for (int m = 0; m < invertedpuzzleMask.mipmapCount; ++m)
        {
            Color[] color = invertedpuzzleMask.GetPixels(m);
            for (int i = 0; i < color.Length; ++i)
            {
                //color[i].r = 1 ;//- color[i].r;
                //color[i].g = 1 ;//- color[i].g;
                //color[i].b = 1 ;//- color[i].b;
                color[i].a = 1 - color[i].a;
            }
            invertedpuzzleMask.SetPixels(color, m);
        }
        invertedpuzzleMask.Apply();
    }

    void MakeNewPuzzlePiece()
    {
        if (pb_existingPuzzlePieces + 1 >= pb_GridDimension.x * pb_GridDimension.y)
        {
            tools_noMoreInventory = true;
            return;
        }
        pb_existingPuzzlePieces++;

        pb_nextGrid = pb_existingPuzzlePieces;
        OnPuzzlePiece_Select();
        tools_noMoreInventory = false; ;
    }

    void AddPieceToBoard()
    {

    }

    void UpdatePuzzleImage()
    {
        puzzleImage = assignPuzzleImage;
    }

    void UpdatePuzzleMask()
    {
        puzzleMask = assignPuzzleMask;
    }



    bool Check_ValidPuzzleObj(UnityEngine.Object obj)
    {
        //Alternative is to check if script exist using getcomponent 
        try
        {
            if (((GameObject)obj).CompareTag("PuzzlePiece"))
            {
                return true;
            }
        }
        catch (NullReferenceException)
        {
            //catch if no objects selected
        }
        catch (InvalidCastException)
        {
            //catch if casting failed
        }
        return false;
    }


}