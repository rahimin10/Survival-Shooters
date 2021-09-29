using UnityEngine;
    using UnitySampleAssets.CrossPlatformInput;

    namespace CompleteProject
    {

    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 6f;            

        Vector3 movement;                   
        Animator anim;                      
        Rigidbody playerRigidbody;          
        int floorMask;                      
        float camRayLength = 100f;         

        private void Awake ()
        {
            //mendapatkan nilai mask dari layer floor
            floorMask = LayerMask.GetMask ("Floor");

            //mendapatkan komponen animator
            anim = GetComponent <Animator> ();

            //mendapatkan komponen rididbody
            playerRigidbody = GetComponent <Rigidbody> ();
        }


        private void FixedUpdate ()
        {
            //get(mendapatkan) nilai input horizontal (-1, 0, 1)
            float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");

            //get nilai input vertikal (-1, 0, 1)
            float v = CrossPlatformInputManager.GetAxisRaw("Vertical");

            Move (h, v);
            Turning ();
            Animating (h, v);
        }

        //method player dapat berjalan
        void Move (float h, float v)
        {
            //set nilai x dan y
            movement.Set (h, 0f, v);
            
            //menormalisasi nilai vektor agar total panjang dari vektor adalah 1
            movement = movement.normalized * speed * Time.deltaTime;

            //move to position
            playerRigidbody.MovePosition (transform.position + movement);
        }


        void Turning ()
        {
            //buat ray dari posisi mouse di layer
            Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

            //buat raycast untuk floorHit
            RaycastHit floorHit;

            //lakukan raycast
            if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
            {
                //mendapatkan vector dari posisi player dan posisi floorHit
                Vector3 playerToMouse = floorHit.point - transform.position;

                playerToMouse.y = 0f;

                //mendapatkan look rotation baru ke hit position
                Quaternion newRotatation = Quaternion.LookRotation (playerToMouse);

                //rotasi player
                playerRigidbody.MoveRotation (newRotatation);
            }
        }

        void Animating (float h, float v)
        {
            bool walking = h != 0f || v != 0f;

            anim.SetBool ("IsWalking", walking);
        }
    }

}