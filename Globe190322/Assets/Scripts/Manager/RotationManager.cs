using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour {

    [SerializeField,Tooltip("地軸を設定する星")]
    private Transform Planet = null;

    [SerializeField,Tooltip("地軸の回転の影響を受けるオブジェクト")]
    private Transform RotTarget = null;

    [SerializeField,Tooltip("地軸の方向を定めるオブジェクト")]
    private Transform Flag = null;

    [SerializeField, Tooltip("地軸を表示するのに使うオブジェクト")]
    private GameObject AxisObj = null;

    [SerializeField,Tooltip("地軸の向き")]
    private Vector3 EarthAxis = Vector3.zero;

    [SerializeField,Tooltip("回転の向き")]
    private Vector3 AxisDir = Vector3.zero;

    [SerializeField,Tooltip("１フレーム間の１度の回転値")]
    private Quaternion q;

    private float Angle = 0f;               //回転速度

    private const float AngleRange = 10f;  //設定回転速度

    private const float cAngle_1 = Mathf.PI / 180f;

    [SerializeField]
    private bool RT = false;
    [SerializeField]
    private bool LT = false;

    //Initialize
    private void Start ()
    {
        AxisDir = Vector3.zero;

        q = Quaternion.AngleAxis(Mathf.PI / 180, EarthAxis).normalized;
        AxisDir = q.eulerAngles;
        if(AxisObj == null) AxisObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

        Set_EarthAxis(Flag.transform.position);
    }
	
    //Update
	private void Update ()
    {
        Set_EarthAxis(Flag.transform.position); //今後抜く

        //入力
        RT = Input.GetButton("RightTrigger");
        LT = Input.GetButton("LeftTrigger");

        if (RT && LT || !RT && !LT) Angle = 0f;
        else if (RT)       Angle = AngleRange;
        else if (LT)       Angle = -AngleRange;
    }

    //FixedUpdate
    private void FixedUpdate()
    {
        if (RotTarget) RotTarget.transform.rotation = Quaternion.AngleAxis(Angle * Time.deltaTime, EarthAxis.normalized) * RotTarget.transform.rotation;  //回転オブジェクトを回転させる

        q = Quaternion.AngleAxis(cAngle_1, EarthAxis).normalized;   //Quaternionの更新
    }

    //デバッグ
    private void DebugMethod()
    {
        if (RotTarget == null) return;
        //Debug用に組んでいるのでUpdate書き変えてもいいよ

        //回転軸の設定
        Debug.DrawRay(Planet.position, EarthAxis, Color.red);                   //地軸のDebug表示

        //1度の回転値を計算
        AxisDir = q.eulerAngles;
    }

    #region Getter Method

    //回転の角度を取得
    public float Get_Rotation()
    {
        return Angle;
    }

    //地軸の取得
    public Vector3 Get_EarthAxis()
    {
        return this.EarthAxis;
    }

    //軸回転の向き
    public Vector3 Get_EarthAxisDir()
    {
        Vector3 vec = Vector3.zero;

        return vec;
    }

    //回転しているか
    public bool Get_AxisRotation()
    {
        return Angle != 0f;
    }

    //AxisX回りの回転
    public float Get_AxisRotX()
    {
        if (q.x == 0f) return 0f;
        //時計回り：反時計回り
        return q.x > 0f ? 1f : -1f;
    }

    //AxisY回りの回転
    public float Get_AxisRotY()
    {
        if (q.y == 0f) return 0f;
        //時計回り：反時計回り
        return q.y > 0f ? 1f : -1f;
    }

    //AxisZ回りの回転
    public float Get_AxisRotZ()
    {
        if (q.z == 0f) return 0f;
        //時計回り：反時計回り
        return q.z > 0f ? 1f : -1f;
    }

    //Axis回転をすべて判定して返す
    public Vector3 Get_AxisRotXYZ()
    {
        return new Vector3(Get_AxisRotX(),Get_AxisRotY(),Get_AxisRotZ());
    }

    #endregion

    #region Setter Method

    //回転軸の設定
    public void Set_EarthAxis(Vector3 FlagPos)
    {
        EarthAxis = FlagPos - Planet.position;  //距離ベクトル
        EarthAxis = EarthAxis.normalized;       //方向ベクトル(地軸)

        AxisObj.transform.position = Planet.position;
        AxisObj.transform.rotation = Quaternion.FromToRotation(Vector3.up, EarthAxis);
    }

    //中心位置の設定
    public void Set_CorePosition(Transform Planet)
    {
        this.Planet = Planet;
    }

    #endregion


}
