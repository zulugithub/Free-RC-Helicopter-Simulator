using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class UI_Informations_Overlay : MonoBehaviour
{
    public GameObject object_to_follow;
    public float engine, autorotation, engine_restart_time, brakes, vortex_ring_state, turbulence, landing_gear, rpm, target_rpm, tailrotor_cyclic, mainrotor_cyclic, speed,
        ground_effect, flap_up;
    public int mode;
    private Image image_engine, image_autorotation, image_engine_restart_time, image_brakes, image_vortex_ring_state, image_turbulence, 
        image_landing_gear, image_ground_effect, image_flap_up;
    private Text text_rpm, text_target_rpm, text_mainrotor_cyclic, text_tailrotor_cyclic, text_speed, text_mode;

    private RawImage sr;
    //private Sprite mySprite;
    private Texture2D tex;
    List<int> values;

    // Start is called before the first frame update
    void Start()
    {
        
        image_engine = gameObject.transform.Find("IconEngine").GetComponent<Image>();
        image_autorotation = gameObject.transform.Find("IconAutorotation").GetComponent<Image>();
        image_engine_restart_time = gameObject.transform.Find("IconEngineRestartTime").GetComponent<Image>();
        image_brakes = gameObject.transform.Find("IconBrakes").GetComponent<Image>();
        image_vortex_ring_state = gameObject.transform.Find("IconVortexRingState").GetComponent<Image>(); ;
        image_turbulence = gameObject.transform.Find("IconTurbulence").GetComponent<Image>();
        image_landing_gear = gameObject.transform.Find("IconLandingGear").GetComponent<Image>();
        image_ground_effect = gameObject.transform.Find("IconGroundEffect").GetComponent<Image>();
        image_flap_up = gameObject.transform.Find("IconFlapUp").GetComponent<Image>();
        text_rpm = gameObject.transform.Find("TextRpm").GetComponent<Text>();
        text_target_rpm = gameObject.transform.Find("TextTargetRpm").GetComponent<Text>();
        text_mainrotor_cyclic = gameObject.transform.Find("TextMainrotorCyclic").GetComponent<Text>();
        text_tailrotor_cyclic = gameObject.transform.Find("TextTailrotorCyclic").GetComponent<Text>();
        text_speed = gameObject.transform.Find("TextSpeed").GetComponent<Text>();
        text_mode = gameObject.transform.Find("TextMode").GetComponent<Text>();

        sr = gameObject.transform.Find("GGG").GetComponent<RawImage>();
        tex = new Texture2D(50, 50);
        tex.filterMode = FilterMode.Point;
        sr.texture = tex;
        //mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        //sr.sprite = mySprite;
        values = new List<int> { };

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.gameObject.SetActive(true);
        this.transform.position = Camera.main.WorldToScreenPoint(object_to_follow.transform.position);

        const float limit = 0.05f;
        image_engine.fillAmount = engine < limit ? 0 : engine;
        image_autorotation.fillAmount = autorotation < limit ? 0 : autorotation;
        image_engine_restart_time.fillAmount = engine_restart_time;
        image_brakes.fillAmount = brakes < limit ? 0 : brakes;
        image_vortex_ring_state.fillAmount = vortex_ring_state < limit ? 0 : vortex_ring_state;
        image_turbulence.fillAmount = turbulence < limit ? 0 : turbulence;
        image_landing_gear.fillAmount = landing_gear < limit ? 0 : landing_gear;
        image_ground_effect.fillAmount = ground_effect < limit ? 0 : ground_effect;
        image_flap_up.fillAmount = flap_up < limit ? 0 : flap_up;
        text_rpm.text = Helper.FormatNumber(rpm, "####") + " rpm";
        text_mainrotor_cyclic.text = "Mr: " + Helper.FormatNumber(mainrotor_cyclic, "####.0") + " °";
        text_tailrotor_cyclic.text = "Tr: " + Helper.FormatNumber(tailrotor_cyclic, "####.0") + " °";
        text_speed.text = Helper.FormatNumber(speed * 3.6f, "###.0")  + " kmh";
        text_mode.text = "Mode " + mode.ToString();
        text_target_rpm.text = Helper.FormatNumber(target_rpm, "####") + " rpm";

        //values.Insert(0, (int)( 25+ Mathf.Sin(Time.time*Mathf.PI*4)*20) );
        values.Insert(0, (int)((Mathf.Abs(rpm) / 3000) * 50 ) );
        if (values.Count > 50) values.Remove(50);

        Helper.SetTexturePixel(ref tex, values);

    }
}
