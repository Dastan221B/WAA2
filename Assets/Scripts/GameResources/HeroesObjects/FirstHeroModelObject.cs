using System.Collections;
using UnityEngine;

public class FirstHeroModelObject : HeroModelObject
{
    [SerializeField] private Animator _hourseAnimation;
    [SerializeField] private Animator _heroAnimation;


    public override void Idle()
    {
        Debug.Log("idle 1");
        _heroAnimation.CrossFade("idle", 0);
        _hourseAnimation.CrossFade("idle", 0);
    }

    public override void Move()
    {

        _heroAnimation.CrossFade("run", 0);
        _hourseAnimation.CrossFade("run", 0);
    }
}