using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;

        public bool isPlayerActive { get; set; }
        public bool isShotKey { get; set; }
        public bool isSlotChangeImageKey { get; set; }
        private int numberSlotChangeImageKey;
        public int NumberSlotChangeImageKey { get { return numberSlotChangeImageKey; } }

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            isPlayerActive = true;
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            if (isPlayerActive)
            {
                // Read the inputs.
                bool crouch = Input.GetKey(KeyCode.LeftControl);
                float h = CrossPlatformInputManager.GetAxis("Horizontal");
                // Pass all parameters to the character control script.
                m_Character.Move(h, crouch, m_Jump);
                m_Jump = false;

                //shooting
                isShotKey = Input.GetKeyDown(KeyCode.Z);

                //ChangeSlotIamge
                isSlotChangeImageKey = CheckSlotChangeImageKey(out numberSlotChangeImageKey);
            }
        }

        private bool CheckSlotChangeImageKey(out int number)
        {
            bool tmp = false;
            number = 0;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                number = 0;
                tmp = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                number = 1;
                tmp = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                number = 2;
                tmp = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                number = 3;
                tmp = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                number = 4;
                tmp = true;
            }

            return tmp;
        }
    }
}
