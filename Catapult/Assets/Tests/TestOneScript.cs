using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestOneScript
    {
        GameObject slingshot;


        [SetUp]
        public void SetUp()
        {
            slingshot = MonoBehaviour.Instantiate(Resources.Load<GameObject>("SlingShot"));
            slingshot.transform.GetChild(0).gameObject.GetComponent<Payload>().testMode = true;
        }


        [TearDown]
        public void TearDown()
        {
            Object.Destroy(slingshot);
        }


        [UnityTest]
        public IEnumerator ExistanceTest()
        {
            Assert.IsNotNull(slingshot);
            yield return null;
        }


        [UnityTest]
        public IEnumerator PayloadFollowsMouse()
        {
            float startPos = slingshot.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>().transform.position.x;

            slingshot.transform.GetChild(0).gameObject.GetComponent<Payload>().setIsBeingPressed(true);

            slingshot.transform.GetChild(0).gameObject.GetComponent<Payload>().movePayload(new Vector2(1000, 0));

            yield return new WaitForSeconds(0.1f);

            float NewPos = slingshot.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>().transform.position.x;

            yield return null;
        }

        [UnityTest]
        public IEnumerator PayLoadReleases()
        {
            Vector2 startPos = slingshot.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>().transform.position;

            slingshot.transform.GetChild(0).gameObject.GetComponent<Payload>().setIsBeingPressed(true);

            slingshot.transform.GetChild(0).gameObject.GetComponent<Payload>().movePayload(new Vector2(1000, 0));

            yield return new WaitForSeconds(0.1f);

            slingshot.transform.GetChild(0).gameObject.GetComponent<SpringJoint2D>().enabled = false;

            yield return new WaitForSeconds(0.5f);

            Vector2 NewPos = slingshot.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>().transform.position;

            Assert.True((startPos.x > NewPos.x) && (startPos.y > NewPos.y));

            yield return null;
        }




        [UnityTest]
        public IEnumerator PlacementTest()
        {
            float startPos = slingshot.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>().transform.position.y;

            slingshot.transform.GetChild(0).gameObject.GetComponent<Payload>().firePoint.transform.position = new Vector2(0, -100);

            slingshot.transform.GetChild(0).gameObject.GetComponent<Payload>().Reload();

            yield return new WaitForSeconds(0.2f);

            float newpos = slingshot.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>().transform.position.y;

            Assert.Less(newpos, startPos);

            yield return null;
        }


        [UnityTest]
        public IEnumerator AutoShootTest()
        {
            Vector2 startPos = slingshot.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>().transform.position;

            slingshot.transform.GetChild(0).gameObject.GetComponent<Payload>().setIsBeingPressed(true);

            slingshot.transform.GetChild(0).gameObject.GetComponent<Payload>().movePayload(new Vector2(1000, 0));

            yield return new WaitForSeconds(0.1f);

            slingshot.transform.GetChild(0).gameObject.GetComponent<SpringJoint2D>().enabled = false;

            yield return new WaitForSeconds(2.5f);

            Vector2 NewPos = slingshot.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>().transform.position;

            Assert.True((startPos.x > NewPos.x) && (startPos.y > NewPos.y));

            yield return null;
        }


        [UnityTest]
        public IEnumerator AudioTest()
        {
            AudioSource sound = slingshot.GetComponent<AudioSource>();
            Assert.IsNotNull(sound);
            yield return null;
        }


        [UnityTest]
        public IEnumerator BreakSpriteTest()
        {
            float startPos = slingshot.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>().transform.position.x;
            
            slingshot.transform.GetChild(0).gameObject.GetComponent<Payload>().setBreakSlingshot(true);

            yield return new WaitForSeconds(0.2f);

            float FinalPos = slingshot.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>().transform.position.x;

            Assert.Less(FinalPos, startPos);
            yield return null;
        }

    }
}
