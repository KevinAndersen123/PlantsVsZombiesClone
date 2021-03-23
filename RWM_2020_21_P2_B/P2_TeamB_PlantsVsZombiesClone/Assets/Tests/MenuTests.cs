using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class MenuTests
    {
        [SetUp]
        public void Setup()
        {
            //Load Menu Scene
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        }

        [TearDown]
        public void Teardown()
        {
            //Destroy All Objects in the scene
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            {
                Object.Destroy(o);
            }
            //Unload the Scene on Completion
            SceneManager.UnloadSceneAsync("MainMenu");
        }

        [UnityTest]
        public IEnumerator CheckManagerSpawned()
        {
            //Wait one second for game object to instantiate
            yield return new WaitForSeconds(1);
            Assert.True(GameObject.Find("Manager"));
        }
    }
}

