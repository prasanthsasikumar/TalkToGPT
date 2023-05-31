using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP2DController : MonoBehaviour
    {
        public GameObject[] Males;
        public GameObject[] Females;    
        public bool Male;
        public bool Female;
        public float walkspeed;
        float turnspeed;
        public float runspeed;
        public float sprintspeed;
        public float jumpforce;
        GameObject Character;
        GameObject Model;
        Transform trans;
        Rigidbody rigid;
        Animator anim;
        Vector3 InputMoveDir;
        float divergence;
        float tospeed;
        float speed;
        float Aspeed;
        float express;
        float grtime;
        bool jumping;
        bool grounded;
        Vector3 dirforw;
        Vector3 dirside;
        float angleforward;
        GameObject currentGroundObject;
        int collisionCount;
        RaycastHit hit;
        Vector3 targetpoint;
        bool active = true;
        float zoom;
        float moveAUX;

        void Start()
        {
            if (!Male && !Female) Male = true;
            if (Male && !Female) Character = Males[Random.Range(0, Males.Length)];
            if (Female && !Male) Character = Females[Random.Range(0, Females.Length)];
            if (Female && Male)
            {
                    int AUX = Random.Range(0, Males.Length + Females.Length);
                    if (AUX < Males.Length) Character = Males[Random.Range(0, AUX)];
                    else Character = Females[Random.Range(0, AUX - Males.Length)];
            }
            if (Character.GetComponent<TPMalePrefabMaker>() != null)
            {
                Character.GetComponent<TPMalePrefabMaker>().Getready();
                Character.GetComponent<TPMalePrefabMaker>().Randomize();
            }
            if (Character.GetComponent<TPFemalePrefabMaker>() != null)
            {
                Character.GetComponent<TPFemalePrefabMaker>().Getready();
                Character.GetComponent<TPFemalePrefabMaker>().Randomize();
            }

            Model = Instantiate(Character, transform.position, transform.rotation, transform);
            Model.transform.Rotate(Vector3.up * 90f);
            Camera.main.transform.position = transform.position + new Vector3(0f, 1.75f, 6f);
            Camera.main.transform.parent = transform;
            Camera.main.transform.LookAt(transform.position + new Vector3(0f, 1.5f, 0f));
            trans = GetComponent<Transform>();
            rigid = GetComponent<Rigidbody>();
            anim = Model.GetComponent<Animator>();
            express = 0f;
            InputMoveDir = transform.right;
            turnspeed = 5f;
        }


        void Update()
        {
            if (active) MoveChar();
            GetInput();

            if (!grounded) grtime += 0.1f;
            else { grtime = 0f; anim.SetBool("Grounded", true); }
            if (grtime > 0.5f) anim.SetBool("Grounded", false);

            zoom = Mathf.Lerp(zoom, Aspeed * 6f, 0.75f * Time.deltaTime);
            Camera.main.transform.position = transform.position + new Vector3(0f, 1.75f, 6f) + new Vector3(0f, 0f, zoom);
            Camera.main.transform.LookAt(transform.position + new Vector3(0f, 1.5f, 0f));
        }        

        private bool IsFloor(Vector3 v)
        {
            float angle = Vector3.Angle(Vector3.up, v);
            return angle < 45f;
        }
        private void OnCollisionStay(Collision collisionInfo)
        {
            foreach (ContactPoint contact in collisionInfo.contacts)
            {
                if (IsFloor(contact.normal))
                {
                    if (!jumping) grounded = true;
                    currentGroundObject = contact.otherCollider.gameObject;
                    return;
                }
                else if (currentGroundObject == contact.otherCollider.gameObject)
                {
                    grounded = false;
                    currentGroundObject = null;
                }
            }
        }
        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject == currentGroundObject)
            {
                grounded = false;
                currentGroundObject = null;
            }
        }


        void Setdir() 
        {
            RaycastHit hit; RaycastHit hit1;

            Physics.Raycast(trans.position + new Vector3(0f, 0.2f, 0f), Vector3.down, out hit);
            Physics.Raycast(trans.position + new Vector3(0f, 0.2f, 0f) + InputMoveDir * 0.175f, Vector3.down, out hit1);

            dirforw = Vector3.Slerp(dirforw, -Vector3.Cross(hit.normal + hit1.normal, Model.transform.right), 18f * Time.deltaTime).normalized;

            angleforward = Vector3.SignedAngle(Model.transform.forward, dirforw, Model.transform.right);
            anim.SetFloat("Angle", angleforward);
        }

        void GetInput()
        {          
            if (active)
            {
                //actions
                if (Input.GetButtonDown("Jump") && grounded) StartCoroutine("Jump");

                //walk run sprint
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
                {

                    InputMoveDir = (-Vector3.right * Input.GetAxis("Horizontal")).normalized;
                    if (Input.GetKeyUp("left shift")) express = 0f;
                    if (Input.GetKey("left shift"))
                    {
                        tospeed = runspeed + express * (sprintspeed - runspeed);
                        Aspeed = Mathf.Lerp(Aspeed, 2f + express, 5f * Time.deltaTime);
                    }
                    else
                    {
                        tospeed = walkspeed;
                        Aspeed = Mathf.Lerp(Aspeed, 1f, 5f * Time.deltaTime);
                    }
                    if (Input.GetKeyDown("left shift") && Aspeed > 1.25f)
                    {
                        express = 1f;
                    }
                    if (Input.GetKeyUp("left shift")) express = 0f;
                }
                else if (Mathf.Abs(Input.GetAxis("Horizontal")) < 1f)
                {
                    tospeed = 0f;
                    if (jumping) Aspeed = 0f;
                    Aspeed = Mathf.Lerp(Aspeed, 0f, 5f * Time.deltaTime);
                }
            }
                if (Input.GetKey("left shift")) anim.SetBool("Running", true);else anim.SetBool("Running", false);
                if (Input.GetKey("a") || Input.GetKey("d")) anim.SetBool("Walking", true); else anim.SetBool("Walking", false);
                
            speed = Mathf.Lerp(speed, tospeed, 4f * Time.deltaTime);
            anim.SetFloat("Aspeed", Aspeed);
            divergence = Mathf.Abs(Vector3.SignedAngle(Model.transform.forward, InputMoveDir, Vector3.up));
            Setdir();

            if (Input.GetKeyDown("r"))
            {
                if (Model.GetComponent<TPFemalePrefabMaker>() != null)
                {
                    Model.GetComponent<TPFemalePrefabMaker>().Getready();
                    Model.GetComponent<TPFemalePrefabMaker>().Randomize();
                }
                if (Model.GetComponent<TPMalePrefabMaker>() != null)
                {
                    Model.GetComponent<TPMalePrefabMaker>().Getready();
                    Model.GetComponent<TPMalePrefabMaker>().Randomize();
                }
            }
        }

        void MoveChar()
        {
            if (grounded)
            {
                if (divergence > 175f)
                {
                    if (Aspeed < 1.5f) StartCoroutine("Turn180");
                    else StartCoroutine("RunTurn180");
                }
                else
                {
                    Quaternion qAUX = Quaternion.LookRotation(InputMoveDir);
                    Model.transform.rotation = Quaternion.Lerp(Model.transform.rotation, qAUX, turnspeed * Time.deltaTime);
                    rigid.velocity = dirforw * speed;
                }
            }
        }

        
        IEnumerator Jump()
        {
            jumping = true;
            grounded = false;
            grtime = 2f;
            if (speed < 0.25)
            {
                anim.Play("Jump");
                yield return new WaitForSeconds(0.125f);
            }
            else
            {
                anim.Play("Runjump");
                yield return new WaitForSeconds(0.01f);
            }
            speed = speed * 0.5f;
            rigid.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            yield return new WaitForSeconds(0.3f);

            jumping = false;
        }
        IEnumerator Turn180()
        {
            anim.CrossFade("Turn180", 0.05f);
            active = false;
            while (divergence > 6f)
            {
                Model.transform.Rotate(Vector3.up * -380f * Time.deltaTime);
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f) { speed = 1f; Aspeed = 1f; anim.SetFloat("Aspeed", 1f); }
                else { speed = 0f; Aspeed = 0f; anim.SetFloat("Aspeed", 0f); }
                yield return null;
            }
            active = true;
        }
        IEnumerator RunTurn180()
        {
            anim.Play("Runturn180");
            active = false;
            while (divergence > 6f)
            {
                Model.transform.Rotate(Vector3.up * -380f * Time.deltaTime);
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f) { speed = 1f; Aspeed = 1f; anim.SetFloat("Aspeed", 1f); }
                else { speed = 0f; Aspeed = 0f; anim.SetFloat("Aspeed", 0f); }
                yield return null;
            }
            active = true;
        }
    }
