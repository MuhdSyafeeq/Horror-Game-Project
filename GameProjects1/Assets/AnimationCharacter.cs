using UnityEngine;

public class AnimationCharacter : MonoBehaviour
{
    public Animator anim;
    public float velocityZ = 0.0f;
    public float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maxWalkVelocity = 0.5f;
    public float maxRunVelocity = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if(gameManager.Instance.isPaused != true)
        {
            bool forwardPressed = Input.GetKey(KeyCode.W);
            bool leftPressed = Input.GetKey(KeyCode.A);
            bool backPressed = Input.GetKey(KeyCode.S);
            bool rightPressed = Input.GetKey(KeyCode.D);
            bool runPressed = Input.GetKey(KeyCode.LeftShift);

            float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;

            // Move & Accelerate
            if (forwardPressed && velocityZ < currentMaxVelocity)
            {
                velocityZ += Time.deltaTime * acceleration;
            }
            if (leftPressed && velocityX > -currentMaxVelocity)
            {
                velocityX -= Time.deltaTime * acceleration;
            }
            if (rightPressed && velocityX < +currentMaxVelocity)
            {
                velocityX += Time.deltaTime * acceleration;
            }
            if (backPressed && velocityZ > -currentMaxVelocity)
            {
                velocityZ -= Time.deltaTime * acceleration;
            }

            // Not Move & Decelerate
            if (!forwardPressed && velocityZ > 0.0f) { velocityZ -= Time.deltaTime * deceleration; }
            if (!backPressed && velocityZ < 0.0f) { velocityZ += Time.deltaTime * deceleration; }
            if (!forwardPressed && !backPressed && velocityZ != 0.0f && (velocityZ > -0.05f && velocityZ < 0.05f))
            {
                velocityZ = 0.0f;
            }

            if (!leftPressed && velocityX < 0.0f) { velocityX += Time.deltaTime * deceleration; }
            if (!rightPressed && velocityX > 0.0f) { velocityX -= Time.deltaTime * deceleration; }
            if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
            {
                velocityX = 0.0f;
            }

            // LOCK FRONT
            if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
            {
                velocityZ = currentMaxVelocity;
            }
            else if (forwardPressed && velocityZ > currentMaxVelocity)
            {
                velocityZ = Time.deltaTime * deceleration;
                if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
                {
                    velocityZ = currentMaxVelocity;
                }
            }
            else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
            {
                velocityZ = currentMaxVelocity;
            }

            // LOCK LEFT
            if(leftPressed && runPressed && velocityX < -currentMaxVelocity)
            {
                velocityX = -currentMaxVelocity;
            }
            else if(leftPressed && velocityX < -currentMaxVelocity)
            {
                velocityX += Time.deltaTime * acceleration;
                if(velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity + 0.05f))
                {
                    velocityX = -currentMaxVelocity;
                }
            }
            else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
            {
                velocityX = -currentMaxVelocity;
            }

            // LOCK RIGHT
            if(rightPressed && runPressed && velocityX > currentMaxVelocity)
            {
                velocityX = currentMaxVelocity;
            }
            else if(rightPressed && velocityX > currentMaxVelocity)
            {
                velocityX -= Time.deltaTime * deceleration;
                if(velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f))
                {
                    velocityX = currentMaxVelocity;
                }
            }
            else if(rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
            {
                velocityX = currentMaxVelocity;
            }


            // LOCK BACK
            if (backPressed && runPressed && velocityZ < -currentMaxVelocity)
            {
                velocityZ = -currentMaxVelocity;
            }
            else if (backPressed && velocityZ < -currentMaxVelocity)
            {
                velocityZ += Time.deltaTime * acceleration;
                if (velocityZ < -currentMaxVelocity && velocityZ > (-currentMaxVelocity + 0.05f))
                {
                    velocityZ = -currentMaxVelocity;
                }
            }
            else if (backPressed && velocityZ > -currentMaxVelocity && velocityZ < (-currentMaxVelocity + 0.05f))
            {
                velocityZ = -currentMaxVelocity;
            }

            anim.SetFloat("inputH", velocityX); //Mathf.Abs(x));
            anim.SetFloat("inputV", velocityZ); //Mathf.Abs(z));
        }
    }
}
