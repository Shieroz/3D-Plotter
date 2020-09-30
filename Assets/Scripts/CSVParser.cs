using System.Collections;
using System.Collections.Generic;

public class CSVParser
{
    private static int numColumn = 4;

    public static float[,] parser(string Path) {
        string file = System.IO.File.ReadAllText(Path);
        string[] lines = file.Trim().Split(new char[]{'\n'});
        int numlines = lines.Length;
        float[,] result = new float[numlines,numColumn];
        for (int i = 0; i < numlines; i++) {
            string[] line = lines[i].Trim().Split(new char[]{','});
            for (int j = 0; j < numColumn; j++) {
                float.TryParse(line[j], out result[i,j]);
            }
        }
        return result;
    }

/* Test method for the parser
    void Start() {
        float[,] parsed = parser(Path);
        for (int i = 0; i < parsed.GetLength(0); i++) {
            for (int j = 0; j < numColumn; j++) {
                Debug.Log(parsed[i,j]);
            }
            Debug.Log("");
        }
    }
*/
}
