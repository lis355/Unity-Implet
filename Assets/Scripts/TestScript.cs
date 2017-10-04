using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace Implet
{
	public class TestScript : MonoBehaviour
	{
        [MenuItem("Implet/Show Persistent Data")]
        static void ShowPersistentData()
        {
            Process.Start(Application.persistentDataPath);
        }

        void Start()
	    {
	        var i = Preferences.GetInteger("test");
	        i++;
            Preferences.SetInteger("test", i);

            print(i);
	    }
	}
}
