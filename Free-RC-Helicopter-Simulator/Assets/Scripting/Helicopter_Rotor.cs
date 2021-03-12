using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Common;
using System.Xml;
using System.Xml.Serialization;
using Parameter;



namespace Rotor
{


    // ##################################################################################                                                                                                                                                                                                                   
    //    VVVVVVVV           VVVVVVVVIIIIIIIIII   SSSSSSSSSSSSSSS UUUUUUUU     UUUUUUUU           AAA               LLLLLLLLLLL                                     lllllll                                                    
    //    V::::::V           V::::::VI::::::::I SS:::::::::::::::SU::::::U     U::::::U          A:::A              L:::::::::L                                     l:::::l                                                    
    //    V::::::V           V::::::VI::::::::IS:::::SSSSSS::::::SU::::::U     U::::::U         A:::::A             L:::::::::L                                     l:::::l                                                    
    //    V::::::V           V::::::VII::::::IIS:::::S     SSSSSSSUU:::::U     U:::::UU        A:::::::A            LL:::::::LL                                     l:::::l                                                    
    //     V:::::V           V:::::V   I::::I  S:::::S             U:::::U     U:::::U        A:::::::::A             L:::::L                       cccccccccccccccc l::::l   aaaaaaaaaaaaa      ssssssssss       ssssssssss   
    //      V:::::V         V:::::V    I::::I  S:::::S             U:::::D     D:::::U       A:::::A:::::A            L:::::L                     cc:::::::::::::::c l::::l   a::::::::::::a   ss::::::::::s    ss::::::::::s  
    //       V:::::V       V:::::V     I::::I   S::::SSSS          U:::::D     D:::::U      A:::::A A:::::A           L:::::L                    c:::::::::::::::::c l::::l   aaaaaaaaa:::::ass:::::::::::::s ss:::::::::::::s 
    //        V:::::V     V:::::V      I::::I    SS::::::SSSSS     U:::::D     D:::::U     A:::::A   A:::::A          L:::::L                   c:::::::cccccc:::::c l::::l            a::::as::::::ssss:::::ss::::::ssss:::::s
    //         V:::::V   V:::::V       I::::I      SSS::::::::SS   U:::::D     D:::::U    A:::::A     A:::::A         L:::::L                   c::::::c     ccccccc l::::l     aaaaaaa:::::a s:::::s  ssssss  s:::::s  ssssss 
    //          V:::::V V:::::V        I::::I         SSSSSS::::S  U:::::D     D:::::U   A:::::AAAAAAAAA:::::A        L:::::L                   c:::::c              l::::l   aa::::::::::::a   s::::::s         s::::::s      
    //           V:::::V:::::V         I::::I              S:::::S U:::::D     D:::::U  A:::::::::::::::::::::A       L:::::L                   c:::::c              l::::l  a::::aaaa::::::a      s::::::s         s::::::s   
    //            V:::::::::V          I::::I              S:::::S U::::::U   U::::::U A:::::AAAAAAAAAAAAA:::::A      L:::::L         LLLLLL    c::::::c     ccccccc l::::l a::::a    a:::::assssss   s:::::s ssssss   s:::::s 
    //             V:::::::V         II::::::IISSSSSSS     S:::::S U:::::::UUU:::::::UA:::::A             A:::::A   LL:::::::LLLLLLLLL:::::L    c:::::::cccccc:::::cl::::::la::::a    a:::::as:::::ssss::::::ss:::::ssss::::::s
    //              V:::::V          I::::::::IS::::::SSSSSS:::::S  UU:::::::::::::UUA:::::A               A:::::A  L::::::::::::::::::::::L     c:::::::::::::::::cl::::::la:::::aaaa::::::as::::::::::::::s s::::::::::::::s 
    //               V:::V           I::::::::IS:::::::::::::::SS     UU:::::::::UU A:::::A                 A:::::A L::::::::::::::::::::::L      cc:::::::::::::::cl::::::l a::::::::::aa:::as:::::::::::ss   s:::::::::::ss  
    //                VVV            IIIIIIIIII SSSSSSSSSSSSSSS         UUUUUUUUU  AAAAAAA                   AAAAAAALLLLLLLLLLLLLLLLLLLLLLLL        ccccccccccccccccllllllll  aaaaaaaaaa  aaaa sssssssssss      sssssssssss    
    // ##################################################################################
    class Helicopter_Rotor_Visualization_And_Audio
    {

        // ##################################################################################
        //  PPPPPPPPPPPPPPPPP                                                                                                           tttt                                                      
        //  P::::::::::::::::P                                                                                                       ttt:::t                                                      
        //  P::::::PPPPPP:::::P                                                                                                      t:::::t                                                      
        //  PP:::::P     P:::::P                                                                                                     t:::::t                                                      
        //    P::::P     P:::::Paaaaaaaaaaaaa  rrrrr   rrrrrrrrr   aaaaaaaaaaaaa      mmmmmmm    mmmmmmm       eeeeeeeeeeee    ttttttt:::::ttttttt        eeeeeeeeeeee    rrrrr   rrrrrrrrr       
        //    P::::P     P:::::Pa::::::::::::a r::::rrr:::::::::r  a::::::::::::a   mm:::::::m  m:::::::mm   ee::::::::::::ee  t:::::::::::::::::t      ee::::::::::::ee  r::::rrr:::::::::r      
        //    P::::PPPPPP:::::P aaaaaaaaa:::::ar:::::::::::::::::r aaaaaaaaa:::::a m::::::::::mm::::::::::m e::::::eeeee:::::eet:::::::::::::::::t     e::::::eeeee:::::eer:::::::::::::::::r     
        //    P:::::::::::::PP           a::::arr::::::rrrrr::::::r         a::::a m::::::::::::::::::::::me::::::e     e:::::etttttt:::::::tttttt    e::::::e     e:::::err::::::rrrrr::::::r    
        //    P::::PPPPPPPPP      aaaaaaa:::::a r:::::r     r:::::r  aaaaaaa:::::a m:::::mmm::::::mmm:::::me:::::::eeeee::::::e      t:::::t          e:::::::eeeee::::::e r:::::r     r:::::r    
        //    P::::P            aa::::::::::::a r:::::r     rrrrrrraa::::::::::::a m::::m   m::::m   m::::me:::::::::::::::::e       t:::::t          e:::::::::::::::::e  r:::::r     rrrrrrr    
        //    P::::P           a::::aaaa::::::a r:::::r           a::::aaaa::::::a m::::m   m::::m   m::::me::::::eeeeeeeeeee        t:::::t          e::::::eeeeeeeeeee   r:::::r                
        //    P::::P          a::::a    a:::::a r:::::r          a::::a    a:::::a m::::m   m::::m   m::::me:::::::e                 t:::::t    tttttte:::::::e            r:::::r                
        //  PP::::::PP        a::::a    a:::::a r:::::r          a::::a    a:::::a m::::m   m::::m   m::::me::::::::e                t::::::tttt:::::te::::::::e           r:::::r                
        //  P::::::::P        a:::::aaaa::::::a r:::::r          a:::::aaaa::::::a m::::m   m::::m   m::::m e::::::::eeeeeeee        tt::::::::::::::t e::::::::eeeeeeee   r:::::r                
        //  P::::::::P         a::::::::::aa:::ar:::::r           a::::::::::aa:::am::::m   m::::m   m::::m  ee:::::::::::::e          tt:::::::::::tt  ee:::::::::::::e   r:::::r                
        //  PPPPPPPPPP          aaaaaaaaaa  aaaarrrrrrr            aaaaaaaaaa  aaaammmmmm   mmmmmm   mmmmmm    eeeeeeeeeeeeee            ttttttttttt      eeeeeeeeeeeeee   rrrrrrr                  
        // ##################################################################################
        #region parameter
        // ##################################################################################
        private GameObject rotor_object;

        private Material helicopter_rotor_material;
        private GameObject rotorsystem;
        private MeshRenderer rotor_blades_to_deform_at_low_rpm;
        private GameObject rotordisk;
        private GameObject rotordisk_BEMT;
        private Material rotor_blades_material;

        private AudioSource rotor_audio_source_rotor;
        private AudioSource rotor_audio_source_stall;
        
        // conical deformation of rotor disc
        private Mesh rotordisk_deforming_mesh;
        private Vector3[] rotordisk_original_vertices, rotordisk_displaced_vertices;
        readonly List<Vector3[]> list__rotordisk_original_vertices = new List<Vector3[]>();
        private float[] rotordisk_radial_distance_to_center;

        // deformation of rotor blades at low rpm
        private  Mesh rotorblades_deforming_mesh;
        private Vector3[] rotorblades_original_vertices, rotorblades_displaced_vertices;
        readonly List<Vector3[]> list__rotorblades_original_vertices = new List<Vector3[]>();
        private float[] rotorblades_radial_distance_to_center;

        // rotor visibility
        private Material rotordisk_top_material;
        private Material rotordisk_bottom_material;
        #endregion
        // ##################################################################################






        // ##################################################################################
        //                                                                                                                              dddddddd                 
        //    MMMMMMMM               MMMMMMMM                             tttt         hhhhhhh                                          d::::::d                 
        //    M:::::::M             M:::::::M                          ttt:::t         h:::::h                                          d::::::d                 
        //    M::::::::M           M::::::::M                          t:::::t         h:::::h                                          d::::::d                 
        //    M:::::::::M         M:::::::::M                          t:::::t         h:::::h                                          d:::::d                  
        //    M::::::::::M       M::::::::::M    eeeeeeeeeeee    ttttttt:::::ttttttt    h::::h hhhhh          ooooooooooo       ddddddddd:::::d     ssssssssss   
        //    M:::::::::::M     M:::::::::::M  ee::::::::::::ee  t:::::::::::::::::t    h::::hh:::::hhh     oo:::::::::::oo   dd::::::::::::::d   ss::::::::::s  
        //    M:::::::M::::M   M::::M:::::::M e::::::eeeee:::::eet:::::::::::::::::t    h::::::::::::::hh  o:::::::::::::::o d::::::::::::::::d ss:::::::::::::s 
        //    M::::::M M::::M M::::M M::::::Me::::::e     e:::::etttttt:::::::tttttt    h:::::::hhh::::::h o:::::ooooo:::::od:::::::ddddd:::::d s::::::ssss:::::s
        //    M::::::M  M::::M::::M  M::::::Me:::::::eeeee::::::e      t:::::t          h::::::h   h::::::ho::::o     o::::od::::::d    d:::::d  s:::::s  ssssss 
        //    M::::::M   M:::::::M   M::::::Me:::::::::::::::::e       t:::::t          h:::::h     h:::::ho::::o     o::::od:::::d     d:::::d    s::::::s      
        //    M::::::M    M:::::M    M::::::Me::::::eeeeeeeeeee        t:::::t          h:::::h     h:::::ho::::o     o::::od:::::d     d:::::d       s::::::s   
        //    M::::::M     MMMMM     M::::::Me:::::::e                 t:::::t    tttttth:::::h     h:::::ho::::o     o::::od:::::d     d:::::d ssssss   s:::::s 
        //    M::::::M               M::::::Me::::::::e                t::::::tttt:::::th:::::h     h:::::ho:::::ooooo:::::od::::::ddddd::::::dds:::::ssss::::::s
        //    M::::::M               M::::::M e::::::::eeeeeeee        tt::::::::::::::th:::::h     h:::::ho:::::::::::::::o d:::::::::::::::::ds::::::::::::::s 
        //    M::::::M               M::::::M  ee:::::::::::::e          tt:::::::::::tth:::::h     h:::::h oo:::::::::::oo   d:::::::::ddd::::d s:::::::::::ss  
        //    MMMMMMMM               MMMMMMMM    eeeeeeeeeeeeee            ttttttttttt  hhhhhhh     hhhhhhh   ooooooooooo      ddddddddd   ddddd  sssssssssss   
        // ##################################################################################
        #region methods
        public void Init_Rotor_Data(ref Helisimulator.Helicopter_ODE helicopter_ODE, ref GameObject Helicopter_Selected, ref string helicopter_name, stru_rotor par_rotor, string rotor_name, int helicopter_id)
        {

            // ##################################################################################
            //
            // ##################################################################################
            if (Helicopter_Selected.transform.Find(rotor_name + "_Model") != null)
            {
                
                // ##################################################################################
                // Init GameObject
                // ##################################################################################
                rotor_object = Helicopter_Selected.transform.Find(rotor_name + "_Model").gameObject.transform.GetChild(0).gameObject;
                // ##################################################################################



                // ##################################################################################
                // Init Texture
                // ##################################################################################
                // for EC135 rotor cover, that uses the same map, as the main hull
                // for AH56 Cheyenne tailrotor hub, that uses the same map, as the main hull
                // for AH56 Cheyenne propller hub, that uses the same map, as the main hull
                if (rotor_object.transform.Find("Canopy_A") != null)
                    helicopter_rotor_material = rotor_object.transform.Find("Canopy_A").gameObject.GetComponent<MeshRenderer>().material;
                // ##################################################################################



                // ##################################################################################
                // Init Audio
                // ##################################################################################
                if (Helicopter_Selected.transform.Find("Audio Source " + rotor_name) != null)
                    rotor_audio_source_rotor = Helicopter_Selected.transform.Find("Audio Source " + rotor_name).gameObject.GetComponent<AudioSource>();
                else
                    rotor_audio_source_rotor = null;

                if (Helicopter_Selected.transform.Find("Audio Source Stall") != null)
                    rotor_audio_source_stall = Helicopter_Selected.transform.Find("Audio Source Stall").gameObject.GetComponent<AudioSource>();
                else
                    rotor_audio_source_stall = null;
                // ##################################################################################



                // ##################################################################################
                // Init rotor-blades visibility 
                // ##################################################################################
                GameObject rotor = Helicopter_Selected.transform.Find(rotor_name + "_Model/" + helicopter_name + "_" + rotor_name).gameObject;
                foreach (Transform each_child in rotor.transform)
                {
                    foreach (Material material in each_child.GetComponent<MeshRenderer>().materials)
                    {
                        //UnityEngine.Debug.Log("rotor_Blades (Instance) found. Mame: " + material.name + "         "  + helicopter_name + "_rotor_Blades (Instance)");
                        if (material.name == rotor_name + "_Blades (Instance)" || material.name == helicopter_name + "_" + rotor_name + "_Blades (Instance)")
                        {
                            rotor_blades_material = material;
                            rotor_blades_material.ToFadeMode();
                            rotor_blades_material.ChangeTransparency(1.0f - helicopter_ODE.par.simulation.gameplay.rotor_disk_transparency.val); // alpha setting
                            break;
                        }
                    }
                }
                // ##################################################################################



                // ##################################################################################
                // Init rotor-disk visibility 
                // ##################################################################################
                rotordisk = Helicopter_Selected.transform.Find(rotor_name + "_Disk").gameObject;
                rotordisk_top_material = rotordisk.GetComponent<MeshRenderer>().materials[0];
                rotordisk_top_material.ChangeTransparency((1.0f - helicopter_ODE.par.simulation.gameplay.rotor_disk_transparency.val));
                rotordisk_bottom_material = rotordisk.GetComponent<MeshRenderer>().materials[1];
                rotordisk_bottom_material.ChangeTransparency((1.0f - helicopter_ODE.par.simulation.gameplay.rotor_disk_transparency.val));
                // ##################################################################################



                // ##################################################################################
                // Init deformation of rotor-disk
                // ##################################################################################
                rotordisk.transform.localPosition = Helper.ConvertRightHandedToLeftHandedVector(par_rotor.posLH.vect3);

                rotordisk_deforming_mesh = rotordisk.GetComponent<MeshFilter>().mesh;

                // store the rotordisk verticles-array in a list, but only once at the first call, so they can be reused to reset the rotordisk to the initial deformation
                while (helicopter_id >= list__rotordisk_original_vertices.Count)
                    list__rotordisk_original_vertices.Add(default); // add empty array of Vector3 as list element    

                if (list__rotordisk_original_vertices[helicopter_id] == null)
                    list__rotordisk_original_vertices[helicopter_id] = rotordisk_deforming_mesh.vertices.Deep_Clone();

                rotordisk_original_vertices = list__rotordisk_original_vertices[helicopter_id].Deep_Clone();
                rotordisk_radial_distance_to_center = new float[rotordisk_original_vertices.Length];
                rotordisk_displaced_vertices = new Vector3[rotordisk_original_vertices.Length];
                for (int i = 0; i < rotordisk_original_vertices.Length; i++)
                {
                    rotordisk_displaced_vertices[i] = rotordisk_original_vertices[i];
                    rotordisk_radial_distance_to_center[i] = rotordisk_original_vertices[i].magnitude; // distance to center
                }
                // ##################################################################################


             
                // ##################################################################################
                // Init deformation of rotor-blades  
                // ##################################################################################
                if (rotor.transform.Find("Rotorblades") != null)
                {
                    rotor_blades_to_deform_at_low_rpm = rotor.transform.Find("Rotorblades").gameObject.GetComponent<MeshRenderer>();// for deformation of blades 

                    rotorblades_deforming_mesh = rotor_blades_to_deform_at_low_rpm.GetComponent<MeshFilter>().mesh;

                    while (helicopter_id >= list__rotorblades_original_vertices.Count)
                        list__rotorblades_original_vertices.Add(default); // fill with empty        

                    if (list__rotorblades_original_vertices[helicopter_id] == null)
                        list__rotorblades_original_vertices[helicopter_id] = rotorblades_deforming_mesh.vertices.Deep_Clone();

                    rotorblades_original_vertices = new Vector3[rotorblades_deforming_mesh.vertices.Length];
                    rotorblades_radial_distance_to_center = new float[rotorblades_original_vertices.Length];
                    rotorblades_displaced_vertices = new Vector3[rotorblades_original_vertices.Length];

                    for (int i = 0; i < rotorblades_deforming_mesh.vertices.Length; i++)
                    {
                        rotorblades_original_vertices[i] = list__rotorblades_original_vertices[helicopter_id][i];
                        rotorblades_displaced_vertices[i] = rotorblades_original_vertices[i];
                        rotorblades_radial_distance_to_center[i] = Mathf.Sqrt(Mathf.Pow(rotorblades_original_vertices[i].x, 2) + Mathf.Pow(rotorblades_original_vertices[i].z, 2)); // distance to center
                    }
                }
                // ##################################################################################



                // ##################################################################################
                // Init rotation of rotor-blades  
                // ##################################################################################
                rotorsystem = Helicopter_Selected.transform.Find(rotor_name + "_Model").gameObject;

                // Set the position of the coordinate system, that holds the 3d-model of the rotor. The rotation (rpm) is done by rotating this
                // coordiante system around it's orign, but the "localRotation" angle (computation done at "rotate rotor blades") is expressed in unity the helicopters local coordiante system.
                rotorsystem.transform.localPosition = Helper.ConvertRightHandedToLeftHandedVector(par_rotor.posLH.vect3);
                // ##################################################################################



                // ##################################################################################
                // BEMT rotor disc
                // ##################################################################################
                if (Helicopter_Selected.transform.Find(rotor_name + "_Disk_BEMT") != null)
                {
                    rotordisk_BEMT = Helicopter_Selected.transform.Find(rotor_name + "_Disk_BEMT").gameObject;
                }
                // ##################################################################################


            }
            else
            {
                rotor_object = null;
            }
                
        }
        // ##################################################################################



        // ##################################################################################
        // change texture
        // ##################################################################################
        public void Update_Rotor_Material(ref Texture canopy_texture)
        {
            if (helicopter_rotor_material != null)
                helicopter_rotor_material.SetTexture("_MainTex", canopy_texture); // Diffuse
        }
        // ##################################################################################



        // ##################################################################################
        // change rotor visiblitiy as a function of rotation velocity
        // ##################################################################################
        public void Update_Rotor_Visiblitiy(ref Helisimulator.Helicopter_ODE helicopter_ODE, stru_rotor par_rotor, float Theta_col, ref float omega)
        {
            if (rotor_object != null)
            {
                const float rotational_speed_where_visiblity_is_full_transparent_const = 1000.0f; // [rpm]

                float rotational_speed_where_visiblity_is_full_transparent = rotational_speed_where_visiblity_is_full_transparent_const / par_rotor.b.val; // [rpm]
                float rotor_disk_transparency = (1.0f - helicopter_ODE.par.simulation.gameplay.rotor_disk_transparency.val);  // [0...1]
                float normalized_speed_for_rotorvisibility, fade_disk_transparency, fade_blade_transparency;

                // rotor
                normalized_speed_for_rotorvisibility = Mathf.Clamp(Mathf.Abs(omega) / (rotational_speed_where_visiblity_is_full_transparent * Common.Helper.Rpm_to_RadPerSec), 0.00f, 1f);

                fade_disk_transparency = Helper.Step(normalized_speed_for_rotorvisibility, 0.3f, 0, 1.0f, 1);
                fade_blade_transparency = Helper.Step(normalized_speed_for_rotorvisibility, 0.2f, 1, 1.0f, 1 - helicopter_ODE.par.simulation.gameplay.rotor_blade_transparency.val);

                rotordisk_top_material.ChangeTransparency(rotor_disk_transparency * fade_disk_transparency); // alpha setting
                rotordisk_bottom_material.ChangeTransparency(rotor_disk_transparency * fade_disk_transparency); // alpha setting
                rotor_blades_material.ChangeTransparency(fade_blade_transparency); // alpha setting

                // faded or transparent materials are rendered in wrong order, therefore set the material back to opaque, if rotating speed is slow. Else the cockpit windows are printed in front of the blades.
                if (fade_blade_transparency == 1)
                    rotor_blades_material.ToOpaqueMode();
                else
                    rotor_blades_material.ToFadeMode();

                // specular highlight effect of rotor disk
                rotordisk_top_material.SetFloat("_CollectiveSpecular", Theta_col / (par_rotor.K_col.val * Mathf.Deg2Rad));
                rotordisk_bottom_material.SetFloat("_CollectiveSpecular", Theta_col / (par_rotor.K_col.val * Mathf.Deg2Rad));
            }
        }
        // ##################################################################################



        // ##################################################################################
        // rotor and stall sound 
        // ##################################################################################
        public void Update_Rotor_Audio(ref Helisimulator.Helicopter_ODE helicopter_ODE, ref float Omega, ref float omega, Vector3 position, Vector3 velocity, bool gl_pause_flag)
        {
            if (rotor_object != null)
            {
                // limit sound by master volume settings
                float pause_activated_reduces_audio_volume = gl_pause_flag ? 0.2f : 1.0f;

                // rotor sound
                if (rotor_audio_source_rotor != null)
                { 
                    rotor_audio_source_rotor.velocityUpdateMode = AudioVelocityUpdateMode.Dynamic;
                    rotor_audio_source_rotor.pitch = Mathf.Abs(omega / (helicopter_ODE.par.transmitter_and_helicopter.helicopter.rotor_sound_recorded_rpm.val * 6 * Mathf.PI / 180f)); // sound recorded at ~ 1280 rpm
                    rotor_audio_source_rotor.volume = Mathf.Abs(omega / (helicopter_ODE.par.transmitter_and_helicopter.helicopter.rotor_sound_recorded_rpm.val * 6 * Mathf.PI / 180f)); // sound recorded at ~ 1280 rpm

                    // unity's doppler effect still sounds weird in build therefore a custum equation is implemented here (doppler effect has to be set to 0 in unity)
                    float velocity_for_doppler_effect = Vector3.Dot(-position.normalized, velocity); // helicopters velocity vector component pointing to camera
                    const float speed_of_sound = 343; // [m / s]
                    rotor_audio_source_rotor.pitch *= (speed_of_sound + 0f) / (speed_of_sound - velocity_for_doppler_effect);

                    rotor_audio_source_rotor.volume *= (helicopter_ODE.par.transmitter_and_helicopter.helicopter.sound_volume.val / 100f) * pause_activated_reduces_audio_volume * (helicopter_ODE.par.simulation.audio.master_sound_volume.val / 100f);
                }

                // stall sound
                if (rotor_audio_source_stall != null)
                {
                    rotor_audio_source_stall.pitch = Mathf.Abs(omega / (helicopter_ODE.par.transmitter_and_helicopter.helicopter.stall_sound_recorded_rpm.val * 6 * Mathf.PI / 180f)); // sound recorded at ~ 1280 rpm       
                    rotor_audio_source_stall.volume = Mathf.Abs(omega / (helicopter_ODE.par.transmitter_and_helicopter.helicopter.stall_sound_recorded_rpm.val * 6 * Mathf.PI / 180f)); // sound recorded at ~ 1280 rpm      

                    float T_max = 4.0f * helicopter_ODE.par.transmitter_and_helicopter.helicopter.mass_total.val * 9.81f; // helicopter_ODE.par.scenery.gravity.val; //[N] ???? TODOOOOOO nicht hier T_max
                    float T_max_transition = T_max * 0.6f; //[N] ????    0.2  ........ 0.8  
                    rotor_audio_source_stall.volume = Helper.Step(Math.Abs(helicopter_ODE.thrust_mr_for_stall_sound), T_max_transition, 0, T_max_transition * 1.3f, 1) * 0.500000f; // TODO  thrust_mr_for_stall_sound

                    rotor_audio_source_stall.volume *= (helicopter_ODE.par.transmitter_and_helicopter.helicopter.sound_volume.val / 100f) * pause_activated_reduces_audio_volume * (helicopter_ODE.par.simulation.audio.master_sound_volume.val / 100f);
                }
            }
        }
        // ##################################################################################



        // ##################################################################################
        // complete rotor rotation (rotor shaft and mechanic with blades, but without rotordisk)
        // ##################################################################################
        public void Update_Rotor_Rotation(stru_rotor par_rotor, ref float Omega)
        {
            if (rotorsystem != null)
                rotorsystem.transform.localRotation =
                  Quaternion.AngleAxis(Omega * 180f / Mathf.PI, Helper.ConvertRightHandedToLeftHandedVector(par_rotor.dirLH.vect3)) *
                  (Helper.ConvertRightHandedToLeftHandedQuaternion(Helper.S123toQuat(par_rotor.oriLH.vect3)));
        }
        // ##################################################################################




        // ##################################################################################
        // rotor deformation
        // ##################################################################################
        public void Update_Rotor_Deformation(ref Helisimulator.Helicopter_ODE helicopter_ODE, stru_rotor par_rotor, float flapping_a_s_LH, float flapping_b_s_LH, float omega, float Omega)
        {
            if (rotor_object != null)
            {

                // ##################################################################################
                // cone angle
                // ##################################################################################
                float cone_angle;
                const float deformation_velocity = 150; // [rpm] below this rotational velocity deformation (due to gravity and missing centrifugal force) will increase
                if (Mathf.Abs(omega) * Helper.RadPerSec_to_Rpm > deformation_velocity)
                {
                    // rotorblades elastic deformation at low rpm speed
                    cone_angle = helicopter_ODE.thrust_mr_for_stall_sound * helicopter_ODE.par.transmitter_and_helicopter.helicopter.visual_effects.mainrotor_running_deformation.val * Mathf.Deg2Rad; // [rad] TODO other variable instead of thrust_mr_for_stall_sound
                }
                else
                {
                    if (Mathf.Abs(omega) < 0.1f)
                        omega = 0;

                    // rotorblades elastic deformation due to rotor load/thrust 
                    float deformation_rad = helicopter_ODE.par.transmitter_and_helicopter.helicopter.visual_effects.mainrotor_idle_deformation.val * Mathf.Deg2Rad; // 
                    // y = ( ( y2 -  y1    ) / ( x2  - x1 ) ) * ( x - x1 ) + y1
                    // y = ( ( 0  -  angle ) / ( rpm -  0 ) ) * ( x -  0 ) + angle 
                    cone_angle = -((-deformation_rad / deformation_velocity) * (Mathf.Abs(omega) * Helper.RadPerSec_to_Rpm) + deformation_rad); // [rad] 
                }
                // ##################################################################################



                // ##################################################################################
                // rotor disk: conical elastic deformation and tilting of rotor-disk due to rotor load/thrust
                // ##################################################################################
                for (int i = 0; i < rotordisk_displaced_vertices.Length; i++)
                    rotordisk_displaced_vertices[i].y = rotordisk_original_vertices[i].y + rotordisk_radial_distance_to_center[i] * (cone_angle); // small angle approximation: removes Tan(cone_angle) --> cone_angle

                rotordisk_deforming_mesh.vertices = rotordisk_displaced_vertices;
                //rotordisk_deforming_mesh.RecalculateNormals();

                // tilting due to flapping of rotor disk
                Quaternion rotor_axis_orientation = Helper.ConvertRightHandedToLeftHandedQuaternion(Helper.S123toQuat(par_rotor.oriLH.vect3));
                rotordisk.transform.localRotation =
                    Quaternion.Euler(-flapping_b_s_LH * Mathf.Rad2Deg, 0f, flapping_a_s_LH * Mathf.Rad2Deg) *
                    rotor_axis_orientation; // Unity uses left handed Euler rotation, space fixed = extrinsic 123 (= intrinsic = body fixed 321)  

                // ##################################################################################



                // ##################################################################################
                // rotor blades: deformation as a function of rotation speed and tilting of rotor-blades 
                // ##################################################################################
                // tilting due to flapping of rotor blades
                // whole rotor is rotated (inclusive hub) in Update_Rotor_Rotation(). Here the rotation of the blades are undone, so that they can be tilted by the 
                // flapping angles relative to the rotor local system LR0. The  rotation has to be then redone again, but around the tilted rotation axis

                Quaternion rotorsystem_inverse = Quaternion.Inverse(Quaternion.AngleAxis(Omega * 180f / Mathf.PI, Vector3.up));
                // flapping of rotor disk (TPP = tilted tip-path plane) and thus also the rotor blades
                Quaternion rotorblades_flapping = Quaternion.Euler(-flapping_b_s_LH * Mathf.Rad2Deg, 0, flapping_a_s_LH * Mathf.Rad2Deg);
                // rotation around the tilted TPP (tilted tip-path plane) - normal vector
                Quaternion rotate = (Quaternion.AngleAxis(Omega * 180f / Mathf.PI, rotorsystem_inverse * rotorblades_flapping * Vector3.up));

                Quaternion all_tranforms = rotate * rotorsystem_inverse * rotorblades_flapping; // = 3.) * 2.) * 1.)

                // deformation and tilting due to flapping
                for (int i = 0; i < rotorblades_displaced_vertices.Length; i++)
                {
                    // reset all three vector components
                    rotorblades_displaced_vertices[i] = rotorblades_original_vertices[i];

                    // conical deformation
                    if (Mathf.Abs(omega) * Helper.RadPerSec_to_Rpm < deformation_velocity)
                        rotorblades_displaced_vertices[i].y = rotorblades_original_vertices[i].y + Mathf.Pow(rotorblades_radial_distance_to_center[i], 2.0f) * cone_angle; // small angle approximation: removes Tan(cone_angle) --> cone_angle
                    else
                        rotorblades_displaced_vertices[i].y = rotorblades_original_vertices[i].y + rotorblades_radial_distance_to_center[i] * cone_angle; // small angle approximation: removes Tan(cone_angle) --> cone_angle

                    rotorblades_displaced_vertices[i] = (all_tranforms) * rotorblades_displaced_vertices[i];

                }
                rotorblades_deforming_mesh.vertices = rotorblades_displaced_vertices;
                // ##################################################################################


            }
        }
        // ##################################################################################
   


        // ##################################################################################
        // rotor deformation BEMT
        // ##################################################################################
        //public void Update_Rotor_Flapping_BEMT(stru_rotor par_rotor, Quaternion )
        //{

        //    if (rotordisk_BEMT != null)
        //    {
        //        // tilting due to flapping of rotor disk
        //        Quaternion rotor_axis_orientation = Helper.ConvertRightHandedToLeftHandedQuaternion(Helper.S123toQuat(par_rotor.oriLH.vect3));

        //        Vector3 temp = helper.A_RT_BEMT(Quaternion qCO, Quaternion qTO, Vector3 anglesRC, Vector3 v3, out float tilting_angle, out Vector3 axis_vector);

        //        rotordisk_BEMT.transform.localRotation =
        //            Quaternion.Euler(-flapping_b_s_LH * Mathf.Rad2Deg, 0f, flapping_a_s_LH * Mathf.Rad2Deg) *
        //            rotor_axis_orientation; // Unity uses left handed Euler rotation, space fixed = extrinsic 123 (= intrinsic = body fixed 321)  

        //    }
        //}
        // ##################################################################################
        #endregion


    }
    // ##################################################################################













    // ##################################################################################                                                                                                                                                                                                            
    //    PPPPPPPPPPPPPPPPP   HHHHHHHHH     HHHHHHHHHYYYYYYY       YYYYYYY   SSSSSSSSSSSSSSS IIIIIIIIII      CCCCCCCCCCCCC   SSSSSSSSSSSSSSS                        lllllll                                                    
    //    P::::::::::::::::P  H:::::::H     H:::::::HY:::::Y       Y:::::Y SS:::::::::::::::SI::::::::I   CCC::::::::::::C SS:::::::::::::::S                       l:::::l                                                    
    //    P::::::PPPPPP:::::P H:::::::H     H:::::::HY:::::Y       Y:::::YS:::::SSSSSS::::::SI::::::::I CC:::::::::::::::CS:::::SSSSSS::::::S                       l:::::l                                                    
    //    PP:::::P     P:::::PHH::::::H     H::::::HHY::::::Y     Y::::::YS:::::S     SSSSSSSII::::::IIC:::::CCCCCCCC::::CS:::::S     SSSSSSS                       l:::::l                                                    
    //      P::::P     P:::::P  H:::::H     H:::::H  YYY:::::Y   Y:::::YYYS:::::S              I::::I C:::::C       CCCCCCS:::::S                   cccccccccccccccc l::::l   aaaaaaaaaaaaa      ssssssssss       ssssssssss   
    //      P::::P     P:::::P  H:::::H     H:::::H     Y:::::Y Y:::::Y   S:::::S              I::::IC:::::C              S:::::S                 cc:::::::::::::::c l::::l   a::::::::::::a   ss::::::::::s    ss::::::::::s  
    //      P::::PPPPPP:::::P   H::::::HHHHH::::::H      Y:::::Y:::::Y     S::::SSSS           I::::IC:::::C               S::::SSSS             c:::::::::::::::::c l::::l   aaaaaaaaa:::::ass:::::::::::::s ss:::::::::::::s 
    //      P:::::::::::::PP    H:::::::::::::::::H       Y:::::::::Y       SS::::::SSSSS      I::::IC:::::C                SS::::::SSSSS       c:::::::cccccc:::::c l::::l            a::::as::::::ssss:::::ss::::::ssss:::::s
    //      P::::PPPPPPPPP      H:::::::::::::::::H        Y:::::::Y          SSS::::::::SS    I::::IC:::::C                  SSS::::::::SS     c::::::c     ccccccc l::::l     aaaaaaa:::::a s:::::s  ssssss  s:::::s  ssssss 
    //      P::::P              H::::::HHHHH::::::H         Y:::::Y              SSSSSS::::S   I::::IC:::::C                     SSSSSS::::S    c:::::c              l::::l   aa::::::::::::a   s::::::s         s::::::s      
    //      P::::P              H:::::H     H:::::H         Y:::::Y                   S:::::S  I::::IC:::::C                          S:::::S   c:::::c              l::::l  a::::aaaa::::::a      s::::::s         s::::::s   
    //      P::::P              H:::::H     H:::::H         Y:::::Y                   S:::::S  I::::I C:::::C       CCCCCC            S:::::S   c::::::c     ccccccc l::::l a::::a    a:::::assssss   s:::::s ssssss   s:::::s 
    //    PP::::::PP          HH::::::H     H::::::HH       Y:::::Y       SSSSSSS     S:::::SII::::::IIC:::::CCCCCCCC::::CSSSSSSS     S:::::S   c:::::::cccccc:::::cl::::::la::::a    a:::::as:::::ssss::::::ss:::::ssss::::::s
    //    P::::::::P          H:::::::H     H:::::::H    YYYY:::::YYYY    S::::::SSSSSS:::::SI::::::::I CC:::::::::::::::CS::::::SSSSSS:::::S    c:::::::::::::::::cl::::::la:::::aaaa::::::as::::::::::::::s s::::::::::::::s 
    //    P::::::::P          H:::::::H     H:::::::H    Y:::::::::::Y    S:::::::::::::::SS I::::::::I   CCC::::::::::::CS:::::::::::::::SS      cc:::::::::::::::cl::::::l a::::::::::aa:::as:::::::::::ss   s:::::::::::ss  
    //    PPPPPPPPPP          HHHHHHHHH     HHHHHHHHH    YYYYYYYYYYYYY     SSSSSSSSSSSSSSS   IIIIIIIIII      CCCCCCCCCCCCC SSSSSSSSSSSSSSS          ccccccccccccccccllllllll  aaaaaaaaaa  aaaa sssssssssss      sssssssssss    
    // ##################################################################################   
    static class Helicopter_Rotor_Physics
    {

        // store the values for three rotors (mainrotor, tailrotor, propeller) 
        // turbulence
        private static List<Vector3> turbulence_force_LH = new List<Vector3> { Vector3.zero, Vector3.zero, Vector3.zero }; // [N]
        private static List<Vector3> turbulence_torque_LH = new List<Vector3> { Vector3.zero, Vector3.zero, Vector3.zero }; // [Nm]
        private static List<Vector3> turbulence_torque_LH_target = new List<Vector3> { Vector3.zero, Vector3.zero, Vector3.zero }; // [Nm] for smooth transition
        private static List<Vector3> turbulence_torque_LH_velocity = new List<Vector3> { Vector3.zero, Vector3.zero, Vector3.zero }; // [Nm] for smooth transition
        private static List<float> turbulence_time_elapsed_old = new List<float> { 0, 0, 0 }; // [sec]
        // vortex ring state
        private static List<float> vortex_ring_state_stength = new List<float> { 0, 0, 0 }; // [0..1]




        // ##################################################################################
        //                                                                                                                              dddddddd                 
        //    MMMMMMMM               MMMMMMMM                             tttt         hhhhhhh                                          d::::::d                 
        //    M:::::::M             M:::::::M                          ttt:::t         h:::::h                                          d::::::d                 
        //    M::::::::M           M::::::::M                          t:::::t         h:::::h                                          d::::::d                 
        //    M:::::::::M         M:::::::::M                          t:::::t         h:::::h                                          d:::::d                  
        //    M::::::::::M       M::::::::::M    eeeeeeeeeeee    ttttttt:::::ttttttt    h::::h hhhhh          ooooooooooo       ddddddddd:::::d     ssssssssss   
        //    M:::::::::::M     M:::::::::::M  ee::::::::::::ee  t:::::::::::::::::t    h::::hh:::::hhh     oo:::::::::::oo   dd::::::::::::::d   ss::::::::::s  
        //    M:::::::M::::M   M::::M:::::::M e::::::eeeee:::::eet:::::::::::::::::t    h::::::::::::::hh  o:::::::::::::::o d::::::::::::::::d ss:::::::::::::s 
        //    M::::::M M::::M M::::M M::::::Me::::::e     e:::::etttttt:::::::tttttt    h:::::::hhh::::::h o:::::ooooo:::::od:::::::ddddd:::::d s::::::ssss:::::s
        //    M::::::M  M::::M::::M  M::::::Me:::::::eeeee::::::e      t:::::t          h::::::h   h::::::ho::::o     o::::od::::::d    d:::::d  s:::::s  ssssss 
        //    M::::::M   M:::::::M   M::::::Me:::::::::::::::::e       t:::::t          h:::::h     h:::::ho::::o     o::::od:::::d     d:::::d    s::::::s      
        //    M::::::M    M:::::M    M::::::Me::::::eeeeeeeeeee        t:::::t          h:::::h     h:::::ho::::o     o::::od:::::d     d:::::d       s::::::s   
        //    M::::::M     MMMMM     M::::::Me:::::::e                 t:::::t    tttttth:::::h     h:::::ho::::o     o::::od:::::d     d:::::d ssssss   s:::::s 
        //    M::::::M               M::::::Me::::::::e                t::::::tttt:::::th:::::h     h:::::ho:::::ooooo:::::od::::::ddddd::::::dds:::::ssss::::::s
        //    M::::::M               M::::::M e::::::::eeeeeeee        tt::::::::::::::th:::::h     h:::::ho:::::::::::::::o d:::::::::::::::::ds::::::::::::::s 
        //    M::::::M               M::::::M  ee:::::::::::::e          tt:::::::::::tth:::::h     h:::::h oo:::::::::::oo   d:::::::::ddd::::d s:::::::::::ss  
        //    MMMMMMMM               MMMMMMMM    eeeeeeeeeeeeee            ttttttttttt  hhhhhhh     hhhhhhh   ooooooooooo      ddddddddd   ddddd  sssssssssss   
        // ##################################################################################
        #region methods

        static public void Rotor_Reset_Variables()
        {
            turbulence_force_LH = new List<Vector3> { Vector3.zero, Vector3.zero, Vector3.zero }; // [N]
            turbulence_torque_LH = new List<Vector3> { Vector3.zero, Vector3.zero, Vector3.zero }; // [Nm]
            turbulence_torque_LH_target = new List<Vector3> { Vector3.zero, Vector3.zero, Vector3.zero }; // [Nm] for smooth transition
            turbulence_torque_LH_velocity = new List<Vector3> { Vector3.zero, Vector3.zero, Vector3.zero }; // [Nm] for smooth transition
            turbulence_time_elapsed_old = new List<float> { 0, 0, 0 }; // [0..1]
            vortex_ring_state_stength = new List<float> { 0, 0, 0 }; // [0..1]
        }

        static public void Rotor_Thrust_and_Torque_with_Precalculations(
            float time,
            float dtime,
            int integrator_function_call_number, 
            Rotor_Type rotor_type,
            bool flag_freewheeling,
            bool invert_rotor_rotation_direction, // par.transmitter_and_helicopter.helicopter.transmission.invert_rotor_rotation_direction.val
            bool calculate_flapping,
            bool calculate_vortex_ring_state,
            bool calculate_turbulence,
            bool calculate_ground_effect,
            stru_rotor par_rotor,
            stru_flapping par_flapping,
            stru_tuning par_tuning,
            stru_rotor_tuning_base par_rotor_tuning,
            float rho_air, // [kg/m^3] par.scenery.weather.rho_air.val
            float mass_total, // [kg] par.transmitter_and_helicopter.helicopter.mass_total.val
            System.Random random,
            Vector3 vectO, // [m] state: helicopter's position, expressed in inertial frame
            Quaternion q, // [-] state: helicopter's orientation 
            Vector3 velo_LH, // [m/sec] state: helicopter's translational velocity vector, expressed in helicopter's local coordinate system
            Vector3 omega_LH, // [rad/sec] state: helicopter's rotational velocity vector, expressed in helicopter's local coordinate system
            double flapping_a_s_LR, // [rad] state: flapping around rotor's local z-axis
            double flapping_b_s_LR, // [rad] state: flapping around rotor's local x-axis
            double omega_shaft, // [rad/sec] state: rotor's shaft speed
            double Theta_col, // [rad] 
            double Theta_cyc_a_s, // [rad] 
            double Theta_cyc_b_s, // [rad] 
            Vector3 velo_windO_LH, // [m/s]
            Vector3 force_fuselageLH, // [N]
            float ground_effect_rotor_hub_distance_to_ground,
            Vector3 ground_effect_rotor_triangle_normalR,
            out double thrust, // [N]
            out double torque, // [Nm]
            out double v_i, // [m/sec] indeuced velocity
            out Vector3 force_at_heli_CH_O, // [N]
            out Vector3 torque_at_heli_CH_LH, // [Nm]
            out double dflapping_a_s_LR__int_dt, // [rad/sec] state derivative
            out double dflapping_b_s_LR__int_dt, // [rad/sec] state derivative
            out float debug_tau_rotor, // flapping time consant
            out Vector3 debug_rotor_forceLH, // left handed system
            out Vector3 debug_rotor_torqueLH, // left handed system
            out Vector3 debug_rotor_positionO, // left handed system
            out Vector3 debug_rotor_forceO, // left handed system
            out Vector3 debug_rotor_torqueO, // left handed system
            out Vector3 debug_rotor_flapping_tilting_torqueO, // left handed system
            out Vector3 debug_rotor_flapping_stiffness_torqueO, // left handed system
            out float debug_power_rotor_pr,
            out float debug_power_rotor_i,
            out float debug_power_rotor_pa,
            out float debug_power_rotor_c,
            out float debug_strength_turbulence,
            out float debug_strength_vortex_ring_state,
            out float debug_strength_ground_effect,
            out float debug_strength_flap_up
            )
        {
            force_at_heli_CH_O = Vector3.zero;
            torque_at_heli_CH_LH = Vector3.zero;

            // ##################################################################################
            // relative to air velocity at the position of the rotor, expressed in the rotor's coordinate system
            // ##################################################################################
            Vector3 r_PRCH_LH = par_rotor.posLH.vect3; // [m] vector from heli's center mass to rotor's position, expressed in helicopter's local frame
            Vector3 omega_CHO_LH = omega_LH; // [rad/s] helicopter's center mass rotational velocity vector relative to inertial frame O, expressed in helicopter's local frame
            //Vector3 dr_CHO_O_dt = veloO; // [m/s] helicopter's center mass translational velocity vector relative to inertial frame O, expressed in inertial frame
            Vector3 dr_CHO_LH_dt = velo_LH; // [m/s] helicopter's center mass translational velocity vector relative to inertial frame O, expressed in helicopter's local frame
            Vector3 v_wO_LH = velo_windO_LH; // [m/s] wind velocity relative to inertial frame O, expressed in helicopter's local frame

            Vector3 v_PRO_LH = dr_CHO_LH_dt - v_wO_LH + Helper.Cross(omega_CHO_LH, r_PRCH_LH); // [m/sec] velocity with wind at rotor hub, expressed in helicopter's local frame 
            Vector3 v_PRO_LR = Helper.A_LR_S123(par_rotor.oriLH.vect3 * Helper.Deg_to_Rad, v_PRO_LH); // [m/sec]  per definition the y-axis is the rotor shaft
            // ##################################################################################



            // ##################################################################################
            // rigid body velocity component at rotor position, that is parallel to gravity direction (for energy based rotor torque calculation, TODO better aproach)
            // ##################################################################################
            Vector3 velo_rotor_hub_O = Helper.A_RL(q, v_PRO_LH); // [m/sec] velocity at rotor position, expressed in inertial frame O
            Vector3 direction_rotor_O = Helper.A_RL(q, par_rotor.dirLH.vect3); // [m]  
            double v_rotor_hub_yO = Math.Abs(Vector3.Dot(direction_rotor_O, new Vector3(0, 1, 0))) * velo_rotor_hub_O.y; // (dot product, projection of main rotor axis to inertial sysetms' y-axis ) * velocity in y direction
            // ##################################################################################
   


            // ##################################################################################
            // Rotor_Thrust_and_Torque
            // ##################################################################################
            Rotor_Thrust_and_Torque(invert_rotor_rotation_direction, par_rotor, par_rotor_tuning, rho_air, mass_total, omega_shaft, Theta_col, v_rotor_hub_yO, v_PRO_LR, force_fuselageLH, flapping_a_s_LR, flapping_b_s_LR, out thrust, out torque, out v_i,
                                out debug_power_rotor_pr,
                                out debug_power_rotor_i,
                                out debug_power_rotor_pa,
                                out debug_power_rotor_c);
            // ##################################################################################





            // ##################################################################################
            // force at helicopter's center of mass (rotor thrust)
            // ##################################################################################
            // local coordinate system force due to tilted tip-path plane (TPP) 
            // around rotor's coordinate system's x axis  --  flapping_b_s_LR
            // around rotor's coordinate system's z axis  --  flapping_a_s_LR
            Vector3 thrust_vector_LR; // [N] in rotor's coordinate system
            Vector3 torque_vector_LR; // [Nm] in rotor's coordinate system 
            if (calculate_flapping)
                thrust_vector_LR = Helper.Az_RL((float)flapping_a_s_LR, Helper.Ax_RL((float)flapping_b_s_LR, new Vector3(0.0f, (float)thrust, 0.0f)));      
            else
                thrust_vector_LR = new Vector3(0.0f, (float)thrust, 0.0f);

            if (!flag_freewheeling) 
                torque_vector_LR = new Vector3(0, (float)torque, 0); // [Nm] in rotor's coordinate system
            else
                torque_vector_LR = new Vector3(0, 0, 0); // [Nm] in rotor's coordinate system
            Vector3 torque_vector_LH = Helper.A_RL_S123(par_rotor.oriLH.vect3 * Helper.Deg_to_Rad, torque_vector_LR); // [Nm] expressed in helicopter's coordinate system

            Vector3 thrust_vector_LH =  Helper.A_RL_S123(par_rotor.oriLH.vect3 * Helper.Deg_to_Rad, thrust_vector_LR); // [N] expressed in helicopter's coordinate system
            Vector3 thrust_vector_O = Helper.A_RL(q, thrust_vector_LH); // [N] epressed in inertial frame

            force_at_heli_CH_O += thrust_vector_O; // [N]
            torque_at_heli_CH_LH += torque_vector_LH; // [Nm]

            debug_rotor_forceLH = Helper.ConvertRightHandedToLeftHandedVector(thrust_vector_LH); // [N] also threat right to lefthandside conv.
            debug_rotor_torqueLH = Helper.ConvertRightHandedToLeftHandedVector(torque_vector_LH); // [N] also threat right to lefthandside conv.
            debug_rotor_positionO = Helper.ConvertRightHandedToLeftHandedVector(vectO + Helper.A_RL(q, par_rotor.posLH.vect3)); // [m] also treat right to lefthandside conv. TODO position-do not update every time
            debug_rotor_forceO = Helper.ConvertRightHandedToLeftHandedVector(thrust_vector_O); //  [N] also threat right to lefthandside conv.
            debug_rotor_torqueO = Helper.ConvertRightHandedToLeftHandedVector(Helper.A_RL(q, torque_vector_LH)); //  [Nm] also threat right to lefthandside conv.
            // ##################################################################################



            // ##################################################################################
            // torque by rotor flapping at helicopter's center of mass
            // ##################################################################################
            // 1.) torque due to tilted tip-path plane (TPP) thrust force in local coordinate system  (but also due to offset position of rotor, relative to helicopter's center of mass)    
            Vector3 torque_tilted_TPP_LH = Helper.Cross(par_rotor.posLH.vect3, thrust_vector_LH);         
            torque_at_heli_CH_LH += torque_tilted_TPP_LH; // [Nm]

            // debug
            debug_rotor_flapping_tilting_torqueO = Helper.ConvertRightHandedToLeftHandedVector(Helper.A_RL(q, torque_at_heli_CH_LH));

            // 2.) torque due to restraint in the blade attachment to the rotor head. (O-ring)
            if (calculate_flapping)
            {
                Vector3 torque_stiffness_LR = Vector3.zero;
                torque_stiffness_LR.x = Nonlinear_Stiffness(par_flapping.hub_stiffness_mr.val, 3f * Mathf.PI / 180.0f, (float)flapping_b_s_LR) * (float)flapping_b_s_LR; // [Nm]
                torque_stiffness_LR.z = Nonlinear_Stiffness(par_flapping.hub_stiffness_mr.val, 3f * Mathf.PI / 180.0f, (float)flapping_a_s_LR) * (float)flapping_a_s_LR; // [Nm]
                Vector3 torque_stiffness_LH = Helper.A_RL_S123(par_rotor.oriLH.vect3 * Helper.Deg_to_Rad, torque_stiffness_LR);
                torque_at_heli_CH_LH += torque_stiffness_LH; // [Nm]

                // debug
                debug_rotor_flapping_stiffness_torqueO = Helper.ConvertRightHandedToLeftHandedVector(Helper.A_RL(q, torque_stiffness_LH));
            }
            else
            {
                debug_rotor_flapping_stiffness_torqueO = Vector3.zero;
            }
            // ##################################################################################



            // ##################################################################################
            // rotor flapping dynamics
            // ##################################################################################
            if (calculate_flapping)
            {
                // Lock number of rotor blade
                double gamma_rotor = (rho_air * par_rotor.c.val * par_rotor.C_l_alpha.val * Mathf.Pow(par_rotor.R.val, 4)) / (par_flapping.I_flapping.val);
                double tau_rotor = 20; // [sec] time constaint of rotor flapping
                if (Math.Abs(omega_shaft) > 0.1)
                    tau_rotor = 16 / (gamma_rotor * Math.Abs(omega_shaft)) / (1 - (8 * par_flapping.e.val) / (3 * par_rotor.R.val));

                Vector3 omega_LR = Helper.A_LR_S123(par_rotor.oriLH.vect3 * Helper.Deg_to_Rad, omega_LH); // express helicopter's local rotation velocity vector in rotor's coordinate system

                dflapping_a_s_LR__int_dt = -omega_LR.z - (1f / tau_rotor) * flapping_a_s_LR + par_flapping.A_b_s.val * flapping_b_s_LR + (1f / tau_rotor) * Theta_cyc_a_s 
                    + par_flapping.A_a_r.val * v_PRO_LR.x; // [rad/sec] pitch flapping velocity a_s (longitudial direction)
                dflapping_b_s_LR__int_dt = -omega_LR.x + par_flapping.B_a_s.val * flapping_a_s_LR - (1f / tau_rotor) * flapping_b_s_LR + (1f / tau_rotor) * Theta_cyc_b_s
                    - par_flapping.B_a_r.val * v_PRO_LR.z; // [rad/sec] roll flapping velocity b_s (lateral direction)

                debug_tau_rotor = (float)tau_rotor;
                //// for low main rotor rotational speeds reduce the effect of flapping
                //dflapping_a_s_LR__int_dt *= reduce_flapping_effect_at_low_rpm;  // TODO
                //dflapping_b_s_LR__int_dt *= reduce_flapping_effect_at_low_rpm;  // TODO

                if (Math.Abs(omega_shaft) < 0.1) 
                { 
                    dflapping_a_s_LR__int_dt = 0; // [rad/sec] 
                    dflapping_b_s_LR__int_dt = 0; // [rad/sec] 
                }
            }
            else
            { 
                dflapping_a_s_LR__int_dt = 0; // [rad/sec] 
                dflapping_b_s_LR__int_dt = 0; // [rad/sec] 
                debug_tau_rotor = 0;
            }
            const float normalize_speed = 100.0f / 3.6f; // [m/sec] 100 kmh
            debug_strength_flap_up = Mathf.Sqrt(v_PRO_LH.x * v_PRO_LH.x + v_PRO_LH.z * v_PRO_LH.z) / normalize_speed; // simplified: shows only speed
            // ##################################################################################



            // ##################################################################################
            // vortex ring state
            // ##################################################################################
            float strength_turbulence = 0;

            float horizontal_speed_LH = Mathf.Sqrt(v_PRO_LH.x * v_PRO_LH.x + v_PRO_LH.z * v_PRO_LH.z); // [m/sec] 
            float vertical_speed_LH = -v_PRO_LH.y * Mathf.Sign((float)v_i); // [m/sec] sign(v_i) --> determine speed and the correct direction

            if (calculate_vortex_ring_state)
            {
                float horizontal_velocityLH = par_tuning.vortex_ring_state_v_horizontal.val; // [m/sec]
                float vertical_velocityLH = par_tuning.vortex_ring_state_v_vertical.val; // [m/sec]
                float force_reduction_factor = par_tuning.vortex_ring_state_force_reduction_factor.val; // if vortex fully developed rotor thrust is reduced at this amount
                float torque_reduction_factor = par_tuning.vortex_ring_state_torque_reduction_factor.val; // if vortex fully developed rotor torque is reduced at this amount
                float elliptic_factor = 1.5f;
                float stretch_x_factor = 1.3f;
                float vortex_ring_state_stength_rising_speed_factor = par_tuning.vortex_ring_state_stength_rising_speed_factor.val;  // todo should depend on time, now depends on ODE-thread frequency



                if (integrator_function_call_number == 0)
                {
                    // In rate of descent vs horizntal speed diagram we define the area, where from 
                    // light to severe turbulence and thrust variation occures. A cosinus curve is 
                    // used to create a smooth transition from areas without VRS to areas with VRS
                    Vector2 vrs_center_in_diagram = new Vector2(horizontal_velocityLH, vertical_velocityLH); // [m/s] horizontal, vertical speed
                    float vrs_radius_in_diagram = vrs_center_in_diagram[0] * stretch_x_factor; // [m/s]

                    float distance_to_vrs_center = new Vector2(horizontal_speed_LH - vrs_center_in_diagram[0], (vertical_speed_LH - vrs_center_in_diagram[1]) * elliptic_factor).magnitude;

                    //if ((horizontal_speed_LH > 1 && horizontal_speed_LH < 5) &&
                    //     (vertical_speed_LH > -10 && vertical_speed_LH < -2) && !flag_freewheeling)
                    if ((distance_to_vrs_center < vrs_radius_in_diagram) && !flag_freewheeling)
                    {
                        vortex_ring_state_stength[(int)rotor_type] += vortex_ring_state_stength_rising_speed_factor * ((Mathf.Cos((distance_to_vrs_center / vrs_radius_in_diagram) * Mathf.PI) + 1f) / 2f);
                    }
                    else
                    {
                        vortex_ring_state_stength[(int)rotor_type] -= vortex_ring_state_stength_rising_speed_factor * 0.5f; // todo descent intensity ???
                    }

                    vortex_ring_state_stength[(int)rotor_type] = Mathf.Clamp(vortex_ring_state_stength[(int)rotor_type], 0, 1); // [0 ... 1]
                }
                force_at_heli_CH_O *= (1 - vortex_ring_state_stength[(int)rotor_type] * force_reduction_factor);
                torque_at_heli_CH_LH *= (1 - vortex_ring_state_stength[(int)rotor_type] * torque_reduction_factor);

                strength_turbulence = vortex_ring_state_stength[(int)rotor_type];
            }
            else
            {
                vortex_ring_state_stength[(int)rotor_type] = 0;
            }
            debug_strength_vortex_ring_state = vortex_ring_state_stength[(int)rotor_type];
            // ##################################################################################



            // ##################################################################################
            // turbulence 
            // ##################################################################################
            if (calculate_turbulence)
            {
                // get the local translational speed
                float translational_velocity_scalar_abs = v_PRO_LH.magnitude; // [m/sec] velo_LH
                // rotation around y-axis (most often rotor shaft axis) has lower turbulence influence.
                float rotational_velocity_scalar_abs = Mathf.Sqrt(1.0f*omega_LH.x*omega_LH.x + 0.2f*omega_LH.y*omega_LH.y + 1.0f*omega_LH.z*omega_LH.z); // [rad/sec]

                // turbulence strength depends on rotational speed but also on translational speed:
                // - if rotational speed is high turbulence is high
                // - but if translational speed is high turbulence is low, even if rotational speed is high (rotor gets undisturbed air)
                strength_turbulence += Helper.Step(rotational_velocity_scalar_abs, par_tuning.turbulence_rotational_velocity_limit.val/2.0f, 0, par_tuning.turbulence_rotational_velocity_limit.val, 1) * 
                                       Helper.Step(translational_velocity_scalar_abs, par_tuning.turbulence_translational_velocity_limit.val/2.0f, 1, par_tuning.turbulence_translational_velocity_limit.val, 0.25f); // [0...1]

                strength_turbulence = Mathf.Clamp(strength_turbulence, 0, 1);

                // turbulence is represented with random values
                float turbulence_time_elapsed = time;
                turbulence_time_elapsed %= 1f / par_tuning.turbulence_frequency.val; // [1/Hz] select the frequency of the turbulence
                if (turbulence_time_elapsed < turbulence_time_elapsed_old[(int)rotor_type])
                {
                    turbulence_force_LH[(int)rotor_type] = new Vector3(
                         ((float)random.NextDouble() - 0.5f) * 2f * strength_turbulence * par_tuning.turbulence_force_strength.vect3.x,
                         ((float)random.NextDouble() - 0.5f) * 2f * strength_turbulence * par_tuning.turbulence_force_strength.vect3.y,
                         ((float)random.NextDouble() - 0.5f) * 2f * strength_turbulence * par_tuning.turbulence_force_strength.vect3.z);  // [N]
                    turbulence_torque_LH_target[(int)rotor_type] = new Vector3(
                        ((float)random.NextDouble() - 0.5f) * 2f * strength_turbulence * par_tuning.turbulence_torque_strength.vect3.x,
                        ((float)random.NextDouble() - 0.5f) * 2f * strength_turbulence * par_tuning.turbulence_torque_strength.vect3.y, 
                        ((float)random.NextDouble() - 0.5f) * 2f * strength_turbulence * par_tuning.turbulence_torque_strength.vect3.z ); // [Nm]
                }
                turbulence_time_elapsed_old[(int)rotor_type] = turbulence_time_elapsed;

                Vector3 turbulence_torque_LH_velocity_ = turbulence_torque_LH_velocity[(int)rotor_type];
                turbulence_torque_LH[(int)rotor_type] = Vector3.SmoothDamp(turbulence_torque_LH[(int)rotor_type], turbulence_torque_LH_target[(int)rotor_type], ref turbulence_torque_LH_velocity_, 0.3F, 1000, (float)dtime);

                force_at_heli_CH_O += Helper.A_RL(q, turbulence_force_LH[(int)rotor_type]);
                torque_at_heli_CH_LH += turbulence_torque_LH[(int)rotor_type];
            }
            else
            {
                vortex_ring_state_stength[(int)rotor_type] = 0;
                turbulence_time_elapsed_old[(int)rotor_type] = 0;
            }
            debug_strength_turbulence = strength_turbulence;
            // ##################################################################################



            // ##################################################################################
            // ground effect 
            // ##################################################################################
            float ground_effect_factor = 1;
            if (calculate_ground_effect)
            {
                // get the factor how perpendicular rotor axis is to surface-triangle and with the sign its direction  
                float ground_effect_mainrotor_perpendicular_factor = Vector3.Dot(direction_rotor_O.normalized * Math.Sign(v_i), -ground_effect_rotor_triangle_normalR);
                //direction_rotor_R = Helper.A_RL(q, par.transmitter_and_helicopter.helicopter.tailrotor.dirLH.vect3).normalized; // [m]  rotor(-y) axis expressed in world coordinates
                //float ground_effect_tailrotor_perpendicular_factor = Vector3.Dot(direction_rotor_R, ground_effect_tailrotor_triangle_normalR);

                float h = ground_effect_rotor_hub_distance_to_ground;   // [m]   h                    height above ground, 
                float V = horizontal_speed_LH;                          // [m/s] V = sqrt(u^2 + w^2)  translational aerodynamic velocity, 
                float R = par_rotor.R.val;                              // [m]   R                    rotor radius
                float v_i_ = Mathf.Abs((float)v_i)*1.0000000000f;       // [m/s] vi                   induced velocity

                // Gareth D. Padfield - HELICOPTER FLIGHT DYNAMICS - equation (3.218)
                // orignal equation has discontinpoutiy at h = R / 4, therefore equation is modified
                if(ground_effect_mainrotor_perpendicular_factor > 0)
                    ground_effect_factor = ((1f / (1f - (1f / 16f) * Mathf.Pow(R / (h + (R / 3.0000000000000f)), 2f) / (1f + Mathf.Pow(V / v_i_, 2f)))) - 1f) * 2f + 1f;

                force_at_heli_CH_O *= ground_effect_factor;
                torque_at_heli_CH_LH *= 1;
                
            }
            debug_strength_ground_effect = (ground_effect_factor-1.0f)*4.0f; // [0...1]
            // ##################################################################################
        }
        // ##################################################################################









        // ##################################################################################
        // calculate rotors thrust and torque
        // ##################################################################################
        static void Rotor_Thrust_and_Torque(bool invert_rotor_torque_direction, stru_rotor par_rotor, stru_rotor_tuning_base par_rotor_tuning, double rho_air, 
            double mass_total, double omega_shaft, double Theta_col, double v_mr_hub_yO, Vector3 velo_rotor_hub_and_windLR, 
            Vector3 force_fuselageLH, double flapping_a_s_LR, double flapping_b_s_LR, out double thrust, out double torque, out double v_i,
                out float debug_power_rotor_pr,
                out float debug_power_rotor_i,
                out float debug_power_rotor_pa,
                out float debug_power_rotor_c)
        {
   
            // thrust and induced velocity (has to be solved iteratively)
            Newton_Raphson(Rotor_Thrust_and_Induced_Velocity, par_rotor, par_rotor_tuning, rho_air, mass_total, omega_shaft, velo_rotor_hub_and_windLR,
                Theta_col, flapping_a_s_LR, flapping_b_s_LR, out thrust, out v_i);
            
            v_i *= 1.50000000000;
            thrust *= 0.90000000000;

            // torque
            Rotor_Torque(par_rotor, par_rotor_tuning, invert_rotor_torque_direction, rho_air, mass_total, omega_shaft, 
                v_mr_hub_yO, velo_rotor_hub_and_windLR, force_fuselageLH, v_i*1.200000000, thrust, out torque, 
                out debug_power_rotor_pr,
                out debug_power_rotor_i,
                out debug_power_rotor_pa,
                out debug_power_rotor_c);


            v_i *= -1;
        }
        // ##################################################################################




        // ##################################################################################
        // Rotor thrust and induced velocity, initially proposed by Heffley (NASA 1986)
        // Cai, Guowei, Chen, Ben M., Lee, Tong Heng  (https://www.springer.com/de/book/9780857296344) 2011_Book_UnmannedRotorcraftSystems.pdf page. 102
        // ##################################################################################
        static private double Rotor_Thrust_and_Induced_Velocity(
            double v_i,  // [m/sec] 
            stru_rotor par_rotor,
            stru_rotor_tuning_base par_rotor_tuning,
            double rho_air,  // [kg/m^3] 
            double mass_total, // par.transmitter_and_helicopter.helicopter.mass_total.val
            double omega_shaft, // [rad/sec] 
            Vector3 velo_rotor_hub_and_wind_LR, // [m/sec] 
            double Theta_col, // [rad] 
            double flapping_a_s_LR, // [rad] 
            double flapping_b_s_LR, // [rad] 
            out double thrust) // [N]    
        {
            double T_max = 20.0 * mass_total * 9.81; // par.scenery.gravity.val; //[N] TODO: factor physical source and put into parameter  
            double T_max_transition = T_max * 0.1; //[N] ????  TODO

            double Cl_alpha_rotor = par_rotor.C_l_alpha.val; // lift curve slope

            double u_a = velo_rotor_hub_and_wind_LR.x; // [m/sec]
            double v_a = velo_rotor_hub_and_wind_LR.y; // [m/sec]  
            double w_a = velo_rotor_hub_and_wind_LR.z; // [m/sec] 

            // ------------------------------------------------------------------------
            // nonphysical tuning
            // ------------------------------------------------------------------------
            u_a *= par_rotor_tuning.inflow_factor.val; // [m/sec]
            v_a *= par_rotor_tuning.inflow_factor.val; // [m/sec]
            w_a *= par_rotor_tuning.inflow_factor.val; // [m/sec]
            u_a += par_rotor_tuning.inflow_offset.val * Math.Sign(u_a); // [m/sec]
            //v_a += par_rotor_tuning.inflow_offset.val * Math.Sign(v_a); // [m/sec]
            w_a += par_rotor_tuning.inflow_offset.val * Math.Sign(w_a); // [m/sec]
            // ------------------------------------------------------------------------

            double a_s = flapping_a_s_LR; // [rad] flapping angle
            double b_s = flapping_b_s_LR; // [rad] flapping angle

            //double Theta_col = par.transmitter_and_helicopter.helicopter.rotor.K_col.val * Mathf.Deg2Rad * delta_col_ + par.transmitter_and_helicopter.helicopter.rotor.Theta_col_0.val * Mathf.Deg2Rad; // [rad] Collective pitch angle of main rotor blade
            double v_r = -v_a + a_s * u_a - b_s * w_a; // sign inverted compared to original docu by Guowei because our coordinate system is different (his +z is our -y) 
            double v_bl = v_r + (2.0 / 3.0) * Math.Abs(omega_shaft) * par_rotor.R.val * Theta_col;

            double v_hat_2 = u_a * u_a + w_a * w_a + v_r * (v_r - 2 * v_i); // intermediate variable

            thrust = ((rho_air * Math.Abs(omega_shaft) * par_rotor.R.val * par_rotor.R.val * Cl_alpha_rotor * par_rotor.b.val * par_rotor.c.val) / 4.0) * (v_bl - v_i);

            thrust = Helper.Limit_Symetric(thrust, T_max, T_max_transition);

            double temp_v_i = Math.Sqrt(Math.Abs(Math.Sqrt(Math.Pow((v_hat_2) / 2.0, 2) + Math.Pow(thrust / (2.0 * rho_air * Math.PI * par_rotor.R.val * par_rotor.R.val), 2)) - (v_hat_2) / 2.0));   // eq 6.13

            if (Math.Sign(thrust) != Math.Sign(temp_v_i))
                temp_v_i = -temp_v_i;

            return -v_i + temp_v_i;
        }
        // ##################################################################################



        // ##################################################################################
        // Rotor torque
        // Cai, Guowei, Chen, Ben M., Lee, Tong Heng  (https://www.springer.com/de/book/9780857296344) 2011_Book_UnmannedRotorcraftSystems.pdf page. 103
        // ##################################################################################
        static private void Rotor_Torque(
            stru_rotor par_rotor,
            stru_rotor_tuning_base par_tuning,
            bool invert_rotor_torque_direction, // par.transmitter_and_helicopter.helicopter.transmission.invert_rotor_rotation_direction.val
            double rho_air,  // [kg/m^3]  
            double mass_total, // [kg] 
            double omega_shaft,  // [rad/sec] 
            double v_rotor_hub_yO,  // [m/sec] 
            Vector3 velo_rotor_hub_and_windLR, // [m/sec] 
            Vector3 force_fuselageLH, // [N] 
            double v_i, // [m/sec]  
            double thrust, // [N]  
            out double torque, // [Nm] 
            out float debug_power_rotor_pr, // [W] 
            out float debug_power_rotor_i, // [W] 
            out float debug_power_rotor_pa, // [W] 
            out float debug_power_rotor_c // [W] 
            )
        {
            double u_a = velo_rotor_hub_and_windLR.x; // [m/s]
            double v_a = velo_rotor_hub_and_windLR.y; // [m/s] 
            double w_a = velo_rotor_hub_and_windLR.z; // [m/s] 

            double N_rotor; // [Nm]
            double P_pr = ((rho_air * Math.Abs(omega_shaft) * par_rotor.R.val *
                par_rotor.R.val * par_rotor.C_D0.val *
                par_rotor.b.val * par_rotor.c.val) / 8.0) *
                (Math.Pow((omega_shaft * par_rotor.R.val), 2) + 4.6 * (u_a * u_a + w_a * w_a));  // [W] rotor profile power
            double P_i = thrust * v_i; // [W] rotor induced power
            double P_pa = Math.Abs(force_fuselageLH.x * u_a) + Math.Abs(force_fuselageLH.y * (-v_a - v_i)) + Math.Abs(force_fuselageLH.z * w_a); // [W] parasite power
            double P_c = mass_total * v_rotor_hub_yO * 9.81f * Helper.Step((float)Math.Abs(v_rotor_hub_yO), 0.001f, 0.0f, 0.01f, 1); // par.scenery.gravity.val;  // [W] climbing power TODOOOOOOOOOOOOOOOOOOOOOOOOoo  -- 

            double P_rotor = P_pr + P_i + P_pa + P_c; // [W] 

            //N = power_rotor / Helper.Step( Mathf.Abs((float)omega), 0.0001f, 0.0000f, 1.0f, (float)omega);

            if (Math.Abs(omega_shaft) > 0.0001)
                N_rotor = P_rotor / Math.Abs(omega_shaft); // [Nm]
            else
                N_rotor = P_rotor / 0.01; /// power_rotor / 100; //  0; // [Nm]

            N_rotor *= Helper.Step(Mathf.Abs((float)omega_shaft), 10f, 0.0f, 20.0f, 1.0f); // for very low rpm (at startup) set the torque to zero - for numerical stability?

            torque = -N_rotor * Mathf.Sign((float)omega_shaft);// * (invert_rotor_torque_direction ? -1.0f : 1.0f); // [Nm]

            debug_power_rotor_pr = (float)P_pr; // rotor profile power
            debug_power_rotor_i = (float)P_i; // rotor induced power
            debug_power_rotor_pa = (float)P_pa; // rotor parasite power
            debug_power_rotor_c = (float)P_c; // rotor climbing power
        }
        // ##################################################################################




        // ##################################################################################
        // rotor flapping creates over "O"-Rings a torque at helicopter: here a nonlinear stiffness is modelled
        // ##################################################################################
        static private float Nonlinear_Stiffness(float stiffness, // [Nm/rad]
                                            float start_nonlinearity, // [rad] 
                                            float flapping_angle) // [rad]
        {
            const float tuning_exponent = 2.0f; // [1...2??]
            if (flapping_angle > start_nonlinearity)
            {
                return (stiffness + Mathf.Pow(stiffness * (flapping_angle - start_nonlinearity), tuning_exponent));
            }
            else
            {
                if (flapping_angle < -start_nonlinearity)
                {
                    return (stiffness + Mathf.Pow(stiffness * Mathf.Abs(flapping_angle + start_nonlinearity), tuning_exponent));
                }
                else
                {
                    return stiffness;
                }
            }
        }
        // ##################################################################################




        // ############################################################################ 
        /// <summary>
        /// Newton-Raphson iterative method (used to solve the induced velocity and thrust equations)
        /// </summary>
        /// <param name="v_i"></param>
        /// <param name="omega"></param>
        /// <param name="velo_and_windL"></param>
        /// <param name="delta_col"></param>
        /// <param name="flapping_a_s_L"></param>
        /// <param name="flapping_b_s_L"></param>
        /// <param name="thrust"></param>
        /// <returns></returns>
        // ############################################################################
        public delegate double MyFunc(double v_i, stru_rotor par_rotor, stru_rotor_tuning_base par_rotor_tuning, double rho_air, double mass_total, double omega_shaft, Vector3 velo_and_windL, double delta_col, double flapping_a_s_LR, double flapping_b_s_LR, out double thrust);
        public static void Newton_Raphson(MyFunc delegateFunc, stru_rotor par_rotor, stru_rotor_tuning_base par_rotor_tuning, double rho_air, double mass_total, double omega_shaft, Vector3 velo_and_windL, double delta_col, double flapping_a_s_LR, double flapping_b_s_LR, out double thrust, out double v_i_out)
        {
            // Newton-Raphson scheme https://en.wikipedia.org/wiki/Newton%27s_method 
            int n = 0; // [-] counter of Newton�Raphson iterations
            const double error_tol = 0.0001; // [N] Newton-Raphson iterations error bound
            const int n_max = 50; // [-] limit maximum Newton-Raphson iterations
            double error = error_tol * 2; // [N] holds Newton-Raphson scheme error value (with any start value > error_tol)
            double v_i_t = 0; // [m/sec] induced velocity
            double v_i_tp1; // [m/sec] next result of Newton-Raphson step v_i_tp1 ==> v_i[t+1]
            double derivFunc; // [N/(m/sec)] Newton�Raphson scheme needs the derivative of the function -==> d g_0 dv_i 
            double v_i_tp1_save = 0; // [N/(m/sec)] use last value if dg_0_dv_i gets close to zero (to avoid division through zero)
            const double f = 0.6f; // [-] newton scheme convergence rate coefficient ( ~0.4 .... 1.0 )
            const double DELTA_v_i = 0.00001; // [-] central differgence slope of function (dg_0_dv_i) in newton scheme is close to zero
            thrust = 0;  // [N] thrust
            v_i_out = 0; // [m/sec] induced velocity

            // Newton�Raphson scheme finds thrust and induced velocity (result over pitch angle is not smooth !! TODO)
            while (error >= error_tol && n++ <= n_max)
            {
                // derivFunc(x) using central difference scheme
                derivFunc = (delegateFunc(v_i_t + DELTA_v_i, par_rotor, par_rotor_tuning, rho_air, mass_total, omega_shaft, velo_and_windL, delta_col, flapping_a_s_LR, flapping_b_s_LR, out _) -
                             delegateFunc(v_i_t - DELTA_v_i, par_rotor, par_rotor_tuning, rho_air, mass_total, omega_shaft, velo_and_windL, delta_col, flapping_a_s_LR, flapping_b_s_LR, out thrust)) / (2.0 * DELTA_v_i);

                // h+1 = h - f * func(x) / derivFunc(x)
                if (Math.Abs(derivFunc) > 0.00001)
                    v_i_tp1 = v_i_t - f * delegateFunc(v_i_t, par_rotor, par_rotor_tuning, rho_air, mass_total, omega_shaft, velo_and_windL, delta_col, flapping_a_s_LR, flapping_b_s_LR, out thrust) / derivFunc;
                else
                    v_i_tp1 = v_i_tp1_save;

                // iteration error
                error = Math.Abs(v_i_tp1 - v_i_t);

                v_i_t = v_i_tp1;
                v_i_tp1_save = v_i_tp1;
            }
            v_i_out = v_i_t;
        }
        // ############################################################################

    }
    // ############################################################################
    #endregion

}
// ############################################################################