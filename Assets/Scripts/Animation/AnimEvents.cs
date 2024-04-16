using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEnded : UnityEvent<int> { }

public class AnimEvents : MonoBehaviour
{
    public AnimationEnded animationEnded;

    public void EndOfAnim (int animEvent)
    {
        if (animEvent == 0)
        {
            animationEnded.Invoke(animEvent);
        }
        
    }
}
