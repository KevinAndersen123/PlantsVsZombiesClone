using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class ProgressBarTests
    {
        //private WaveSpawner waveSpawner;
        public GameObject game;
        public WaveSpawner ws;

        [SetUp]
        public void Setup()
        {
            //Load Demo Scene
            SceneManager.LoadScene("Demo", LoadSceneMode.Additive);
            //Create Spawner in the Scene
            //game = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
            ws = MonoBehaviour.Instantiate(Resources.Load<WaveSpawner>("Prefabs/Spawner"));
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
            SceneManager.UnloadSceneAsync("Demo");
        }


        //Check Progress Bar is spawning correctly
        [UnityTest]
        public IEnumerator spawnProgressBarTest()
        {
            //Wait one second for game object to instantiate
            yield return new WaitForSeconds(1);


            Assert.True(GameObject.Find("ProgressBar"));
            Assert.True(GameObject.Find("Boarder"));
            Assert.True(GameObject.Find("WaveProgress"));
        }

        
        [UnityTest]
        public IEnumerator barFillingTest()
        {
            //Create Progress bar varible
            ProgressBar pb;
            //Assign progress bar from wave spawner
            pb = ws.pb;
            //Make sure progress bar is not null
            if (pb != null)
            {
                //Check its starting Value is 0
                Assert.True(pb.slider.value == 0);

                //Wait one second for game object to instantiate
                yield return new WaitForSeconds(2);

                //Check progress value has increased
                if (pb.slider.value > 0)
                {
                    Assert.Pass();
                }
            }
            else
            {
                //Fail if not found
                Assert.Fail();
            }
        }


        [UnityTest]
        public IEnumerator heavyWaveSpawningIcon()
        {
            yield return new WaitForSeconds(1);

            //Get Game Object with pointer tag
            GameObject heavyWavePointer = GameObject.FindGameObjectWithTag("Pointer");

            //Check for Pointer Spawned
            Assert.True(heavyWavePointer != null);

        }
    }
}
