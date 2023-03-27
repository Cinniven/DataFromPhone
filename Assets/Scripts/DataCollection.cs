using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

public class DataCollection : MonoBehaviour
{
    private int _test = 1;
    string _fileName = "";

    private int _dataSize = 20;
    private float _dataInterval = 0.1f;
    [SerializeField] GameObject button;
    
    public void StartTest()
    {
        InputSystem.EnableDevice(Accelerometer.current);
        _fileName = Application.dataPath + "/Accelerometer_Data.csv";
        Debug.Log("Starting test nr " + _test);
        Debug.Log("Seconds,X,Y,Z");

        TextWriter tw = new StreamWriter(_fileName, true);
        tw.WriteLine("Test nr. " + _test);
        tw.WriteLine("Seconds, X, Y, Z");
        tw.Close();

        button.SetActive(false);
        StartCoroutine(WriteCSV());
    }

    void TestCcomplete()
    {
        Debug.Log("Test Done Succesfully");
        InputSystem.DisableDevice(Accelerometer.current);
        _test++;
        button.SetActive(true);
    }

    IEnumerator WriteCSV()
    {
        TextWriter tw = new StreamWriter(_fileName, true);
        for(int i = 0; i < _dataSize; i++)
        {
            tw.WriteLine((_dataInterval * i).ToString() + "," + Input.acceleration.x.ToString() + "," + Input.acceleration.y.ToString() + "," + Input.acceleration.z.ToString());
            Debug.Log((_dataInterval * i).ToString() + "" + Input.acceleration.x + "" + Input.acceleration.y + "" + Input.acceleration.y);
            yield return new WaitForSeconds(_dataInterval);
        }
        tw.Close();
        TestCcomplete();
    }
}
