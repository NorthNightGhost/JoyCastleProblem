
using UnityEngine;
class Solution : MonoBehaviour {

    enum MoveEnum {
        Normal,
        EaseIn,
        EaseOut,
        EaseInOut
    }

    Transform moveTransform;
    Vector3 beginPos;
    Vector3 targetPos = Vector3.zero;
    float finishAlarm=0,useTime=0;
    MoveEnum moveEnum;
    bool IsBack = false,IsFinish=true;


    void move(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong, MoveEnum _moveEnum= MoveEnum.Normal) {
        moveTransform = gameObject.transform;
        beginPos = begin;
        targetPos = end;
        useTime = time;
        finishAlarm = Time.time + useTime; 
        IsBack = pingpong;
        IsFinish = false;
        moveEnum = _moveEnum;
    }
    private void Awake()
    {
        move(this.gameObject, this.transform.position, this.transform.position+ this.transform.forward * 100, 3f, true,MoveEnum.EaseInOut);
    }

    float moveGap = 0.01f;
    float moveAlarm;
    void  RealMove(float progress) {
        if (Time.time < moveAlarm) return;
        moveAlarm = Time.time + moveGap;

        Vector3 pos=Vector3.zero;
        switch (moveEnum) {
            case MoveEnum.Normal:
                pos = beginPos + (targetPos - beginPos) * progress;
                break;
            case MoveEnum.EaseIn:
                float moveSpeed = (targetPos - beginPos).magnitude /(0.95f* useTime);
                if (progress < 0.1f)
                {
                    pos = beginPos + (targetPos - beginPos).normalized * moveSpeed * progress / 0.1f * progress * useTime * 0.5f;
                }
                else {
                    pos = beginPos + (targetPos - beginPos).normalized * (0.5f * moveSpeed * 0.1f * useTime + (progress - 0.1f) * useTime * moveSpeed);
                }
                break;
            case MoveEnum.EaseOut:
                moveSpeed = (targetPos - beginPos).magnitude / (0.95f * useTime);
                if (progress > 0.9f)
                {
                    pos = beginPos + (targetPos - beginPos).normalized * ((targetPos - beginPos).magnitude- (0.5f * (1 - progress) * useTime * (1 - progress) / 0.1f * moveSpeed));
                }
                else {
                    pos = beginPos + (targetPos - beginPos).normalized * moveSpeed * progress * useTime;
                }
                break;
            case MoveEnum.EaseInOut:
                moveSpeed = (targetPos - beginPos).magnitude / (0.90f * useTime);
                if (progress < 0.1f)
                {
                    pos = beginPos + (targetPos - beginPos).normalized * moveSpeed * progress / 0.1f * progress * useTime * 0.5f;
                }
                else if (progress > 0.9f) {
                    pos = beginPos + (targetPos - beginPos).normalized * ((targetPos - beginPos).magnitude - (0.5f * (1 - progress) * useTime * (1 - progress) / 0.1f * moveSpeed));
                }
                else
                {
                    pos = beginPos + (targetPos - beginPos).normalized * (0.5f * moveSpeed * 0.1f * useTime + (progress - 0.1f) * useTime * moveSpeed);
                }
                break;
        }
        moveTransform.position = pos;
    }



        private void Update()
    {
        if (Time.time <= finishAlarm)
        {
            float progress = (Time.time - (finishAlarm - useTime)) / useTime;
            RealMove(progress);
        }
        else if(!IsFinish){
            moveTransform.position = targetPos;
            if (IsBack) {
                Vector3 tmp = beginPos;     beginPos = targetPos;   targetPos = tmp;
                IsBack = false;
                finishAlarm = Time.time + useTime;
            }
            else
            {
                IsFinish = true;
            }
        }
        
    }
}


