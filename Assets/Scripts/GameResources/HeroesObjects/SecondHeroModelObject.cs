using System.Collections;
using UnityEngine;

public class SecondHeroModelObject : HeroModelObject
{
    [SerializeField] private Animator _hourseAnimation;
    [SerializeField] private Animator _heroAnimation;


    public override void Idle()
    {
        Debug.Log("idle 2");

        _heroAnimation.CrossFade("idle", 0);
        _hourseAnimation.CrossFade("idle", 0);
    }

    public override void Move()
    {
        _heroAnimation.CrossFade("run", 0);
        _hourseAnimation.CrossFade("run", 0);
    }
}