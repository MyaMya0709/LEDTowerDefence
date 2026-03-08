using System;
using UnityEngine;

public class SptSkillEffect : MonoBehaviour
{
    //[Header("애니메이션 자동 재생 옵션")]
    //[SerializeField] private bool playOnEnable = true;
    //[SerializeField] private string stateName = "Play";

    //private Animator _animator;

    // 스킬 쪽에서 넘겨주는 콜백
    private Action _onApply;   // 타격/발사/데미지 등 "효과가 적용되는" 타이밍
    private Action _onEnd;     // 이 이펙트가 끝날 때 실행할 로직 (다음 이펙트 생성 등)

    //private void Awake()
    //{
    //    _animator = GetComponent<Animator>();
    //}

    //private void OnEnable()
    //{
    //    if (playOnEnable && _animator != null)
    //    {
    //        _animator.Play(stateName, 0, 0f);
    //    }
    //}

    /// 스킬 쪽에서 이 이펙트가 무엇을 할지 콜백으로 주입
    public void Init(Action onApply = null, Action onEnd = null)
    {
        _onApply = onApply;
        _onEnd = onEnd;
    }

    // ================================
    //  Animation Event 에서 호출용
    //  (클립 타임라인에 직접 연결)
    // ================================

    /// 애니메이션 중간, "효과가 적용되는 타이밍"에서 호출
    /// 예: 타격 프레임, 투사체 발사, 데미지 박스 생성 등
    public void ApplyEffect()
    {
        _onApply?.Invoke();
    }

    /// 애니메이션이 끝나는 프레임에서 호출
    public void EndEffect()
    {
        _onEnd?.Invoke();
        Destroy(gameObject);
    }
}
