using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestOneScript
    {
        GameObject slingshot = Resources.Load("Catapult") as GameObject;

       // GameObject slingshot = GameObject.Instantiate(Resources.Load("Catapult"));

        Payload payload;


        [SetUp]
        public void SetUp()
        {
           // slingshot = GameObject.Instantiate(Resources.Load("Catapult"));

            //slingshot = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Catapult"));

            payload = slingshot.GetComponent<Payload>();
        }


        [TearDown]
        public void TearDown()
        {
            Object.Destroy(payload);
        }


        [UnityTest]
        public IEnumerator ExistanceTest()
        {
            Assert.IsNotNull(slingshot);
            yield return null;
        }


        [UnityTest]
        public IEnumerator firstTest()
        {
            //float startPos = payload.GetComponent<Rigidbody2D>().transform.position.x;

            // payload.OnMouseUp();

            //yield return new WaitForSeconds(0.05f);

            //float currentPos = payload.GetComponent<Rigidbody2D>().transform.localPosition.x;
            //Assert.Greater(currentPos, startPos);

            yield return null;
        }
    }
}
