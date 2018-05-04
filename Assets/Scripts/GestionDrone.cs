using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ModeDrone { PATROUILLE, ATTAQUE, DÉPLACEMENT_VERS_PISTE_PATROUILLE, DÉPLACEMENT_VERS_MARQUEUR }

public class GestionDrone : MonoBehaviour, Personnage
{
    [SerializeField] AudioClip SonRotor;
    [SerializeField] AudioClip SonDétectionJoueur;
    [SerializeField] AudioClip SonTirLaser;
    [SerializeField] AudioClip SonExplosion;
    [SerializeField] AudioClip SonMarqueur;

    public Vector3 PositionMarqueur;
    Vector3 PositionPointPlusProchePiste;

    GameObject Joueur;
    GameObject gameObjectDrone;

    Rigidbody drone;
    Collider colliderDrone;
    LineRenderer lineRenderer;
    GestionPathfinding gestionPathfinding;
    GestionSolDrone gestionSolDrone;

    public ModeDrone Mode;
    [SerializeField] ModeDrone ModeInitial;

    bool laserTiré; //Si un laser de ce drone est présentement dans l'environnement ou non
    float tempsDepuisTirLaser;
    const float DIAMÈTRE_LASER = 0.5f;
    Vector3 origineLaser;

    float tempsDepuisVérouillageCible;
    const float DÉLAI_VÉROUILLAGE_CIBLE = 0.5f; //Le délai de temps entre le vérouillage de la la cible et le tir à cette position.
    bool cibleVérouillée;
    Vector3 positionCible;

    const float DÉLAI_TIR_LASER = 0.8f; //Le temps que le rayon laser prend pour est projeté après avoir trouvé la position de la cible
    const float DÉLAI_RECHARGE_TIR_LASER = 1.5f;
    const float TEMPS_TIR_LASER = 0.1f; //Le temps pour lequel un laser reste actif
    const int DISTANCE_LASER_MAX = 50; //La distance maximale à laquelle un drone peut être pour tirer un laser

    const int HAUTEUR_NORMALE = 10; //La hauteur par défaut à laquelle le drone flotte
    const float MAX_DISTANCE_DELTA = 0.45f;

    const int NB_DEGRÉS_FOV = 90;
    const int DISTANCE_VISION_MAX = 50; //La distance maximale à laquelle le drone peut appercevoir le joueur

    List<PistePatrouille> ListePistesPatrouille = new List<PistePatrouille>();
    List<Vector2> ListePointsPatrouille;
    int IndicePositionPiste;

    List<Vector2> ListePointsPathfinding = new List<Vector2>(); //La liste des points des noeuds à parcourir pour arriver à un point donné
    int IndicePositionPathfinding;

    //public Vector3 MarqueurÀAtteindre;
    bool NoeudsLesPlusProchesTrouvés;
    bool NoeudInitialLePlusProcheAtteint;
    GameObject NoeudInitial;
    GameObject NoeudFinal;

    public bool PatrouilleEnSensHoraire; //Vrai si le drone patrouille en sens horaire, faux si il patrouille en sens anti-horaire
    [SerializeField] bool PatrouilleEnSensHoraireInitiallement;

    const int DÉPLACEMENT_X = 0; //10;
    const int DÉPLACEMENT_Z = 0; //18;

    public const int VIE_INITIALE = 50;
    int vie;

    const float TEMPS_ARRÊT_ATTAQUE = 7.5f;
    bool DroneArrêté;
    float tempsDepuisArrêt;
    ModeDrone DernierMode;

    bool PointPisteLePlusPrèsTrouvé = false;

    // Use this for initialization
    void Start()
    {
        Joueur = GameObject.Find("Personnage");

        TrouverPistesPatrouille();

        //Devra changer en fonction du niveau
        ListePointsPatrouille = ListePistesPatrouille[0].GetPointsDePatrouille();
        //

        //Au début, trouve la piste la moins couteuse en déplacement et s'y rend... Ignorer ligne suivante surement.
        //pistePatrouille = GameObject.Find("PistePatrouille").GetComponent<PistePatrouille>();

        drone = GetComponent<Rigidbody>();
        gameObjectDrone = drone.gameObject;
        colliderDrone = GetComponent<Collider>();
        lineRenderer = GetComponent<LineRenderer>();
        gestionPathfinding = GetComponent<GestionPathfinding>();
        gestionSolDrone = GameObject.Find("SolDrone").GetComponent<GestionSolDrone>();

        Resetter();

        //Pour faire des tests :

        //Mode = ModeDrone.DÉPLACEMENT_VERS_MARQUEUR;
        ////MarqueurÀAtteindre = new Vector3(-90, 0, 0);

        //
    }

    /// <summary>
    /// Fonction qui fait revenir le drone à ses valeurs initiales
    /// </summary>
    public void Resetter()
    {
        PatrouilleEnSensHoraire = PatrouilleEnSensHoraireInitiallement;
        Mode = ModeInitial;

        DernierMode = Mode;

        IndicePositionPiste = 0;
        IndicePositionPathfinding = 0;
        //transform.position = ListePointsPatrouille[IndicePositionPiste];

        DroneArrêté = false;
        NoeudsLesPlusProchesTrouvés = false;
        NoeudInitialLePlusProcheAtteint = false;

        laserTiré = false;
        vie = VIE_INITIALE;
        tempsDepuisTirLaser = DÉLAI_RECHARGE_TIR_LASER;
    }

    void TrouverPistesPatrouille()
    {
        GameObject[] tableauPistesPatrouille = FindObjectsOfType<GameObject>().Where<GameObject>(x => x.name == "PistePatrouille").ToArray<GameObject>();
        //GameObject[] tableauPistesPatrouille = FindObjectsOfType<GameObject>().Where<GameObject>(x => x.name.Substring(0, 5) == "Piste").ToArray<GameObject>();

        foreach(GameObject g in tableauPistesPatrouille)
        {
            ListePistesPatrouille.Add(g.GetComponent<PistePatrouille>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Mode);

        if(Vie <= 0)
        {
            AudioSource.PlayClipAtPoint(SonExplosion, Joueur.transform.position);
            gameObject.SetActive(false);
        }
    }

    //    if(DernierMode != ModeDrone.DÉPLACEMENT_VERS_PISTE_PATROUILLE)
    //{
    //    DernierMode = ModeDrone.DÉPLACEMENT_VERS_PISTE_PATROUILLE;
    //}
    //else
    //{
    //    DernierMode = ModeDrone.DÉPLACEMENT_VERS_PISTE_PATROUILLE;
    //}
    private void FixedUpdate()
    {
        GérerHauteurAutomatique();

        switch (Mode)
        {
            case ModeDrone.PATROUILLE:

                GérerDétectionJoueur();
                Patrouiller();

                break;

            case ModeDrone.ATTAQUE:

                if(!DroneArrêté)
                {
                    DroneArrêté = true;
                    tempsDepuisArrêt = 0;
                }
                else
                {
                    tempsDepuisArrêt += Time.deltaTime;

                    GérerAttaque();

                    if (tempsDepuisArrêt >= TEMPS_ARRÊT_ATTAQUE) //Si le joueur n'est pas visible : retourner au dernier mode
                    {
                        if (!VérifierJoueurVisible())
                        {
                            Mode = DernierMode;
                            //Mode = ModeDrone.DÉPLACEMENT_VERS_PISTE_PATROUILLE;
                        }
                        else
                        {
                            tempsDepuisArrêt = 0;
                        }
                    }
                }

                break;
            
            case ModeDrone.DÉPLACEMENT_VERS_MARQUEUR:

                DernierMode = ModeDrone.DÉPLACEMENT_VERS_PISTE_PATROUILLE;
                GérerDétectionJoueur();
                GérerDéplacementVersPointPathfinding(PositionMarqueur);

                break;

            case ModeDrone.DÉPLACEMENT_VERS_PISTE_PATROUILLE:

                GérerDétectionJoueur();

                if(!PointPisteLePlusPrèsTrouvé)
                {
                    TrouverPointPlusProchePistePatrouille();
                    Debug.Log("Point plus proche : " + PositionPointPlusProchePiste);
                }

                DernierMode = ModeDrone.PATROUILLE;
                GérerDéplacementVersPointPathfinding(PositionPointPlusProchePiste);

                //Mode = ModeDrone.DÉPLACEMENT_VERS_MARQUEUR; //Quoi faire ici? Ne continuera pas... Correct dans un sens?

                //if(!PointDePisteLePlusPrèsTrouvé)
                //{
                //GérerDéplacementVersPistePatrouille();
                //Mode = ModeDrone.DÉPLACEMENT_VERS_MARQUEUR; //Quoi faire ici? Ne continuera pas... Correct dans un sens?
                //}

                //if (transform.position == PositionMarqueur)
                //{
                //    Mode = ModeDrone.PATROUILLE;
                //    PointDePisteLePlusPrèsTrouvé = false;
                //}

                break;
        }
    }

    void GérerHauteurAutomatique()
    {
        RaycastHit hitSol;
        RaycastHit hitPlafond;
        Ray raySol = new Ray(transform.position + Vector3.down * 5, Vector3.down);
        //Ray raySol = new Ray(colliderDrone.ClosestPointOnBounds(transform.position + Vector3.down * 100), Vector3.down);
        Ray rayPlafond = new Ray(transform.position + Vector3.up * 5, Vector3.up);
        //Ray rayPlafond = new Ray(colliderDrone.ClosestPointOnBounds(transform.position + Vector3.up * 100), Vector3.up);
        Physics.Raycast(raySol, out hitSol);
        Physics.Raycast(rayPlafond, out hitPlafond);

        if (hitSol.collider != null)
        {
            if(hitPlafond.collider == null)
            {
                drone.transform.position = Vector3.MoveTowards(drone.transform.position, hitSol.point + Vector3.up * HAUTEUR_NORMALE, MAX_DISTANCE_DELTA);
            }
            else
            {
                if (Vector3.Distance(hitSol.point, hitPlafond.point) <= 2 * HAUTEUR_NORMALE)
                {
                    drone.transform.position = Vector3.MoveTowards(drone.transform.position, hitSol.point + (Vector3.up * Vector3.Distance(hitPlafond.point, hitSol.point) / 2), MAX_DISTANCE_DELTA);
                }
                else //(Vector3.Distance(transform.position, hitSol.point) < HAUTEUR_NORMALE)
                {
                    drone.transform.position = Vector3.MoveTowards(drone.transform.position, hitSol.point + Vector3.up * HAUTEUR_NORMALE, MAX_DISTANCE_DELTA);
                }
            }
        }
    }

    /// <summary>
    /// Fonction qui détermine la position latérale du drone selon un chemin de patrouille prédéterminé
    /// </summary>
    void Patrouiller()
    {
        //Debug.Log("IP" + IndicePositionPiste);
        //Debug.Log("IP206 = " + ListePointsPatrouille[206]);

        //Debug.Log(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), ListePointsPathfinding[IndicePositionPiste]));

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), ListePointsPatrouille[IndicePositionPiste]) > MAX_DISTANCE_DELTA)
        //if (!((transform.position.x == ListePointsPatrouille[IndicePositionPiste].x) && (transform.position.z == ListePointsPatrouille[IndicePositionPiste].y)))
        //if (new Vector2(transform.position.x, transform.position.z) != ListePointsPatrouille[IndicePositionPiste])
        {
            DéplacerVersPoint(ListePointsPatrouille[IndicePositionPiste]);
        }
        else
        {
            if (PatrouilleEnSensHoraire)
            {
                IndicePositionPiste--;

                if (IndicePositionPiste <= 0)
                {
                    IndicePositionPiste = ListePointsPatrouille.Count - 1;
                }
            }
            else
            {
                IndicePositionPiste++;

                if (IndicePositionPiste == ListePointsPatrouille.Count)
                {
                    IndicePositionPiste = 0;
                }
            }
        }
    }

    void GérerDétectionJoueur()
    {
        if (VérifierJoueurVisible()) //&& Mode == ModeDrone.PATROUILLE
        {
            AudioSource.PlayClipAtPoint(SonDétectionJoueur, transform.position);

            DernierMode = Mode;

            Mode = ModeDrone.ATTAQUE;
            Debug.Log("Le joueur est détecté");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Mode == ModeDrone.PATROUILLE && other.gameObject.tag != "Boutton")
        {
            Debug.Log("Changement de sens de patrouille");

            //Changer pour faire listePointsPatrouille.Reverse à la place?
            PatrouilleEnSensHoraire = !PatrouilleEnSensHoraire;
        }
    }

    public void DéplacerVersPoint(Vector2 pointÀAtteindre)
    {
        transform.LookAt(new Vector3(pointÀAtteindre.x, transform.position.y, pointÀAtteindre.y));

        //Changer hauteur normale trouver un moyen de déplacer sans hauteur.
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(pointÀAtteindre.x, HAUTEUR_NORMALE, pointÀAtteindre.y), MAX_DISTANCE_DELTA);
        //Vector2 position2d = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.z), pointÀAtteindre, MAX_DISTANCE_DELTA);
        //transform.position = new Vector3(position2d.x, HAUTEUR_NORMALE, position2d.y);
        //transform.position = Vector2.MoveTowards(transform.position, new Vector3(pointÀAtteindre.x, 0, pointÀAtteindre.y), MAX_DISTANCE_DELTA);
    }

    /// <summary>
    /// Fonction qui vérifie si le joueur est dans le champs de vision du drone
    /// </summary>
    /// <returns>Vrai si le joueur est visible. Faux sinon</returns>
    bool VérifierJoueurVisible()
    {
        bool voitJoueur = false;

        //Vérification que le joueur est dans le champs de vue
        if (Vector3.Angle(transform.forward, Joueur.transform.position - transform.position) <= (float)NB_DEGRÉS_FOV / 2 && Vector3.Distance(transform.position, Joueur.transform.position) <= DISTANCE_VISION_MAX)
        {
            //Vérification que le joueur n'est pas caché derrière un objet
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Joueur.transform.position - transform.position);
            Physics.Raycast(ray, out hit);

            if (hit.rigidbody.gameObject == Joueur)
            {
                voitJoueur = true;
            }
        }

        return voitJoueur;
    }

    void GérerAttaque()
    {
        if (Vector3.Distance(transform.position, Joueur.transform.position) <= DISTANCE_LASER_MAX)
        {
            transform.forward = Vector3.RotateTowards(transform.forward, Joueur.transform.position - transform.position, MAX_DISTANCE_DELTA, MAX_DISTANCE_DELTA);

            if (tempsDepuisTirLaser >= DÉLAI_RECHARGE_TIR_LASER)
            {
                if (!cibleVérouillée)
                {
                    positionCible = ViserCible();
                    cibleVérouillée = true;
                    tempsDepuisVérouillageCible = 0;
                }

                if (tempsDepuisVérouillageCible >= DÉLAI_VÉROUILLAGE_CIBLE)
                {
                    lineRenderer.enabled = true;
                    AudioSource.PlayClipAtPoint(SonTirLaser, transform.position);
                    TirerLaser(positionCible);
                    tempsDepuisTirLaser = 0;
                    cibleVérouillée = false;
                    laserTiré = true;
                }
            }
        }

        if (laserTiré && tempsDepuisTirLaser >= TEMPS_TIR_LASER)
        {
            ArrêterLaser();
            laserTiré = false;
        }

        tempsDepuisTirLaser += Time.deltaTime;
        tempsDepuisVérouillageCible += Time.deltaTime;
    }

    void GérerDéplacementVersPointPathfinding(Vector3 pointÀAtteindre)
    {
        if(!NoeudsLesPlusProchesTrouvés)
        {
            NoeudInitial = TrouverNoeudPlusProche(transform.position);
            NoeudFinal = TrouverNoeudPlusProche(pointÀAtteindre);
            NoeudsLesPlusProchesTrouvés = true;
        }

        //Debug.Log("Nom :" + TransformNoeudInitial.name + "X = " + TransformNoeudInitial.position.x + "et Z = " + TransformNoeudInitial.position.z);

        if (!NoeudInitialLePlusProcheAtteint)
        {
            //Debug.Log(TransformNoeudInitial.position + TransformNoeudInitial.name);

            //if ((transform.position.x != TransformNoeudInitial.position.x) && (transform.position.z != TransformNoeudInitial.position.z))
            if (!((transform.position.x == NoeudInitial.transform.position.x) && (transform.position.z == NoeudInitial.transform.position.z)))
            {
                //Debug.Log("X : " + TransformNoeudInitial.position.x + "et Z : " + TransformNoeudInitial.position.z);

                //Pourrait améliorer écriture
                DéplacerVersPoint(new Vector2(NoeudInitial.transform.position.x, NoeudInitial.transform.position.z));
            }
            else
            {
                NoeudInitialLePlusProcheAtteint = true;

                //Debug.Log("Parcours pathfinding : " + NoeudInitial + " à " + NoeudFinal);

                List<Vector3> listePointsPathfinding3d = gestionPathfinding.TrouverCheminPlusCourt(NoeudInitial, NoeudFinal);
                listePointsPathfinding3d.Add(pointÀAtteindre);

                //Debug.Log("Count ici : " + listePointsPathfinding3d.Count);

                //
                //Debug.Log("Liste points pathfinding");
                //Debug.Log(listePointsPathfinding3d[0]);
                //int z = 0;
                //foreach(Vector3 v in listePointsPathfinding3d)
                //{
                //    Debug.Log(z + " : " + v);

                //}
                //

                for(int i = 0; i < listePointsPathfinding3d.Count; i++)
                {
                    ListePointsPathfinding.Add(new Vector2(listePointsPathfinding3d[i].x, listePointsPathfinding3d[i].z));
                    Debug.Log("Liste de points pathfinding : point # " + i + "est égal à : " + new Vector2(listePointsPathfinding3d[i].x, listePointsPathfinding3d[i].z));
                }
            }
        }
        else
        {
            //if(!((transform.position.x == ListePointsPathfinding[IndicePositionPiste].x) && (transform.position.z == ListePointsPathfinding[IndicePositionPiste].y)))
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), ListePointsPathfinding[IndicePositionPathfinding]) > MAX_DISTANCE_DELTA)
            {
                DéplacerVersPoint(ListePointsPathfinding[IndicePositionPathfinding]);
            }
            else
            {
                IndicePositionPathfinding++;
            }

            if(IndicePositionPathfinding == ListePointsPathfinding.Count)
            {
                NoeudsLesPlusProchesTrouvés = false;
                NoeudInitialLePlusProcheAtteint = false;

                PointPisteLePlusPrèsTrouvé = false;
                IndicePositionPathfinding = 0;

                AudioSource.PlayClipAtPoint(SonMarqueur, Joueur.transform.position);

                Mode = ModeDrone.ATTAQUE;

                ListePointsPathfinding.Clear();
            }
        }
    }

    /// <summary>
    /// Trouve le point le plus proche du drone dans l'une des pistes présentes et le détermine comme marqueur
    /// </summary>
    void TrouverPointPlusProchePistePatrouille()
    {
        List<Vector2> listePointsPisteTraitée;
        float plusPetiteDistance = float.MaxValue;
        float distance;
        Vector2 position2D = new Vector2(transform.position.x, transform.position.z);
        Vector2 pointDePisteLePlusPrès = Vector3.zero;

        for (int i = 0; i < ListePistesPatrouille.Count; i++)
        {
            listePointsPisteTraitée = ListePistesPatrouille[i].GetPointsDePatrouille();

            for (int j = 0; j < listePointsPisteTraitée.Count; j++)
            {
                distance = Vector2.Distance(position2D, listePointsPisteTraitée[j]);

                if(distance < plusPetiteDistance)
                {
                    plusPetiteDistance = distance;

                    pointDePisteLePlusPrès = listePointsPisteTraitée[j];

                    IndicePositionPiste = j;
                }
            }
        }

        PositionPointPlusProchePiste = new Vector3(pointDePisteLePlusPrès.x, 0, pointDePisteLePlusPrès.y);
        
    }

    /// <summary>
    /// Trouve le noeud le plus proche d'une position 3d dans la liste de transform des noeuds du sol
    /// </summary>
    /// <param name="position"></param>
    /// <returns>Le transform du noeud de la grille le plus proche</returns>
    GameObject TrouverNoeudPlusProche(Vector3 position)
    {
        List<GameObject> listeNoeuds = gestionSolDrone.ListeNoeuds;

        GameObject NoeudPlusProche = listeNoeuds[0];
        float distanceNoeudPlusProche = float.MaxValue;
        float distanceNoeudActuel;

        for (int i = 0; i < listeNoeuds.Count; i++)
        {
            if(listeNoeuds[i].GetComponent<ScriptNoeud>().EstDisponible())
            {
                distanceNoeudActuel = Vector3.Distance(position, listeNoeuds[i].transform.position);

                if (distanceNoeudActuel < distanceNoeudPlusProche)
                {
                    //Debug.Log("Distance du noeud nommé " + listeNoeuds[i].name + " est égale à : " + Vector3.Distance(position, listeNoeuds[i].position));
                    NoeudPlusProche = listeNoeuds[i];

                    distanceNoeudPlusProche = distanceNoeudActuel;
                }
            }
        }

        //Debug.Log(NoeudPlusProche.name);
        return NoeudPlusProche;
    }

    Vector3 ViserCible()
    {
        return Joueur.transform.position;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position">La position vers laquelle le laser est tiré</param>
    void TirerLaser(Vector3 positionCible)
    {
        origineLaser = transform.position + (3f) * transform.forward - (1.20f) * transform.up; //Valeurs pour bien enligner le rayon avec le fusil

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, origineLaser);
        lineRenderer.startWidth = DIAMÈTRE_LASER;
        lineRenderer.endWidth = DIAMÈTRE_LASER;
        lineRenderer.SetPosition(1, positionCible);

        RaycastHit hit;
        Ray ray = new Ray(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0));
        Physics.Raycast(ray, out hit);

        if(hit.rigidbody != null)
        {
            if (hit.rigidbody.gameObject.name == "Drone")
            {
                hit.rigidbody.gameObject.GetComponent<GestionDrone>().Vie -= 40;
            }
            if (hit.rigidbody.gameObject.name == "Personnage")
            {
                hit.rigidbody.gameObject.GetComponent<GestionVieJoueur>().Vie -= 40;
            }
        }

        //colliderLaser = GetComponent<BoxCollider>();
        //colliderLaser.enabled = false;
        //colliderLaser.size = new Vector3(diamètreLaser, diamètreLaser, Vector3.Distance(joueur.transform.position, origineLaser));
        //colliderLaser.transform.position = origineLaser + (0.5f) * (joueur.transform.position - origineLaser);
        //colliderLaser.transform.LookAt(joueur.transform.position);
    }

    void ArrêterLaser()
    {
        lineRenderer.positionCount = 0;
        lineRenderer.enabled = false;
    }

    public int Vie
    {
        get
        {
            return vie;
        }
        set
        {
            if (value <= 0)
            {
                    vie = 0;
            }
            else
            {
                if (value < Vie && Mode != ModeDrone.ATTAQUE)
                {
                    DernierMode = Mode;

                    transform.LookAt(Joueur.transform.position);
                    Mode = ModeDrone.ATTAQUE;
                }
                vie = value;
            }
        }
    }
}
