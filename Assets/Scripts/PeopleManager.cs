using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PeopleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0.01f;
    public float maxhealth;
    public float curHealth;
    public float healthDecreaseAmount;
    public List<int> positionIndexList;
    public float offsetRange = 0.002f;
    Vector3 leftDirection = new Vector3(0, -90, 0);
    Vector3 rightDirection = new Vector3(0, 90, 0);
    Vector3 upDirection = new Vector3(0, 180, 0);
    Vector3 downDirection = new Vector3(0, -180, 0);
    Color originalSkinColor;
    public GameObject rotatingPart;
    public GameObject modelParent;
    public GameObject character;
    public GameObject dangerousIcon;
    public int peopleIndex;
    public int isUnderUmbrella = 0;
    public List<GameObject> umbrellaObject;
    public float reachMoney ;
    public GameObject healEffect;
    public Material skinMat;
    public GameObject healthBar;
    public GameObject getHitEffect;
    public GameObject deathEffect;
    void Start()
    {
        curHealth = maxhealth;
        originalSkinColor = character.GetComponent<MeshRenderer>().materials[0].GetColor("_BaseColor") ;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MoveStart()
    {
        ChangeRotation(0);
        transform.parent = LevelSpawner.Instance.currentLevelScript.roadObjectList[0].transform;
        transform.DOLocalMove(Vector3.zero, 1f).OnComplete(() =>
        {
            MoveToNextIndex(1);
        });
        MoveShake();
    }
    public void MoveToNextIndex(int index)
    {
        if (index == LevelSpawner.Instance.currentLevelScript.roadObjectList.Count - 1)// if it reach to sea
        {
            Debug.Log("qwe");
            transform.parent = LevelSpawner.Instance.currentLevelScript.seaJumpingPos.transform;
            transform.DOLocalJump(new Vector3(Random.Range(-offsetRange, offsetRange), 0, 0), 3, 1, 1f).OnComplete(() =>
            {
                gameObject.SetActive(false);
                GameManager.Instance.currentMoney += 20;
                GameDataManager.Instance.totalMoney += (int)reachMoney;
                UIManager.Instance.moneyText.text= GameDataManager.Instance.totalMoney.ToString();
                GameManager.Instance.currentReachedPeople++;
                UIManager.Instance.swimmingPeopleText.text = GameManager.Instance.currentReachedPeople.ToString();
                UIManager.Instance.moneyParticle.SetActive(true);
            });
            return;
        }
        transform.parent = LevelSpawner.Instance.currentLevelScript.roadObjectList[index].transform;
        if (maxhealth != curHealth && curHealth > 0)
        {
            float redRatio = (maxhealth - curHealth) / maxhealth;
            //float termometerRedRatio = (maxhealth - curHealth+1) / maxhealth;
            character.transform.GetComponent<MeshRenderer>().materials[0].DOKill();
            character.transform.GetComponent<MeshRenderer>().materials[0].DOVector(new Vector4(240f/255f, (213 - 213 * redRatio)/255f, (208 - 208 * redRatio)/255f, 1), "_BaseColor", 0.1f);
            healthBar.transform.DOScaleY(redRatio*1.8f, 0.5f);
        }
        transform.DOLocalMove(new Vector3(0, 0, 0.0017f), speed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() =>
        {
            if (isUnderUmbrella > 0)
            {
                isUnderUmbrella -= 1;
                if(isUnderUmbrella == 0)
                {
                    umbrellaObject[0].GetComponent<hatBullet>().FallOf();
                    umbrellaObject.RemoveAt(0);
                }
            }
            else
            {
                curHealth -= healthDecreaseAmount;
                getHitEffect.SetActive(true);
            }
            if (curHealth == 0)
            {
                Debug.Log("sa");
                Instantiate(deathEffect,transform.position+Vector3.up,deathEffect.transform.rotation);
                GameManager.Instance.currentBurnedPeople++;
                healthBar.transform.parent.gameObject.SetActive(false);
                UIManager.Instance.burnedPeopleText.text = GameManager.Instance.currentBurnedPeople.ToString();
                modelParent.transform.DOKill();
                transform.DOKill();
                transform.parent = null;
                transform.DOLocalRotate(new Vector3(0,0,-90), 0.5f).OnComplete(() =>
                {
                    AmbulanceGenerator.Instance.CreateAmbulance(gameObject);
                });
                transform.DOMoveY(13, 0.5f);
                PeopleGenerator.Instance.peopleObjectList.Remove(gameObject);
                return;
            }
            if (curHealth == 1)
            {
                if (dangerousIcon != null)
                {
                    dangerousIcon.SetActive(true);
                }
            }
            MoveToNextIndex(index + 1);
            ChangeRotation(index);
        });

    }

    public void ChangeRotation(int index)
    {
        rotatingPart.transform.DOLookAt(LevelSpawner.Instance.currentLevelScript.roadObjectList[index + 1].transform.position, 0.2f);
    }

    public void MoveShake()
    {
        /*modelParent.transform.DOLocalRotate(new Vector3(0, 0, -5.5f), curHealth / maxhealth).OnComplete(() =>
        {
            modelParent.transform.DOLocalRotate(new Vector3(0, 0, 5.5f), curHealth / maxhealth).OnComplete(() => MoveShake());
        });*/
    }
    public void CoolOf(int healthIncrease)
    {
        if (curHealth < maxhealth)
        {
            curHealth+= healthIncrease;
            float redRatio = (maxhealth - curHealth) / maxhealth;
            healEffect.SetActive(true);
            character.transform.GetComponent<MeshRenderer>().materials[0].DOKill();
            character.transform.GetComponent<MeshRenderer>().materials[0].DOVector(new Vector4(240f / 255f, (213 - 213 * redRatio) / 255f, (208 - 208 * redRatio) / 255f, 1), "_BaseColor", 0.1f);
            healthBar.transform.DOKill();
            healthBar.transform.DOScaleY(redRatio * 1.8f, 0.1f);
        }
        else
        {
            Debug.Log("blablalbal");
        }
    }
}
