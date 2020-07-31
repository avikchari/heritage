using UnityEditor;              //Inherit Editor functionality
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System;                   //Catch nullreference exceptions
//inherit from editor
public class PuzzlePieceEditor : EditorWindow
{
    public Texture tex_greyBox;
    public UnityEngine.Object selectedPuzzle;

    [MenuItem("CustomTools/PuzzlePieceEditor")]
    public static void Open_PuzzlePieceEditor()
    {
        EditorWindow.GetWindow(typeof(PuzzlePieceEditor));
    }

    public void OnGUI()
    {
        GUILayout.BeginHorizontal();
            //Space to view graphic appearance Puzzle Piece (control collision bounds, preview sprite - fore/background)
            GUILayout.BeginVertical(tex_greyBox, GUIStyle.none, GUILayout.Width(128), GUILayout.Height(128));
                GUILayout.Label("Puzzle Piece View", EditorStyles.boldLabel);


                GUILayout.Space(256);
                GUILayout.Label("Puzzle Pivot and Size");
                EditorGUILayout.RectField(new Rect(new Vector2(0, 0), new Vector2(128, 128)));
            GUILayout.EndVertical();

            //Settings 
            GUILayout.BeginVertical();
            {
                GUILayout.Label("Target Puzzle Object", EditorStyles.boldLabel);
                selectedPuzzle = EditorGUILayout.ObjectField(selectedPuzzle, typeof(GameObject), true);

                if (Check_ValidPuzzleObj(selectedPuzzle))
                {
                    GUILayout.Label("Attributes", EditorStyles.boldLabel);


                }
                else
                {
                    GUILayout.Label("Non-puzzle Piece Gameobject selected");
                }
            }
            GUILayout.EndVertical();

        GUILayout.EndHorizontal();
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
        catch(NullReferenceException)
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
