using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        public static int NumberSlotKey { get; set; }

        private PlatformerCharacter2D m_Character;
        private bool m_Jump;

        public bool IsShotKey { get; set; }

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            NumberSlotKey = -1;
        }


        private void Update()
        {
            if (!GameController.IsInputEnable) return;

            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            //ChangeSlotIamge
            PlayerController.IsSlotChangeImageKey = CheckSlotChangeImageKey();
        }


        private void FixedUpdate()
        {
            if (!GameController.IsInputEnable) return;

            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;

            //shooting
            IsShotKey = Input.GetKeyDown(KeyCode.Z);
        }

        private bool CheckSlotChangeImageKey()
        {
            bool clickedKey = false;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                NumberSlotKey = CheckActiveSlot(0);
                clickedKey = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                NumberSlotKey = CheckActiveSlot(1);
                clickedKey = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                NumberSlotKey = CheckActiveSlot(2);
                clickedKey = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                NumberSlotKey = CheckActiveSlot(3);
                clickedKey = true;
            }
            return clickedKey;
        }

        private int CheckActiveSlot(int idx) {
            return NumberSlotKey == idx ? -1 : idx;
        }
    }
}
