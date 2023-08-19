using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame.Utils
{
    public class RandomObjectPlacement : MonoBehaviour
    {
        public GameObject[] objectPrefabs; // Yerleştirilecek prefab objelerin dizisi
        public int numberOfObjects = 10; // Yerleştirilecek obje sayısı
        public Vector3 areaSize = new Vector3(10f, 1f, 10f); // Yerleştirme alanının boyutları
        public float minYPosition = 1f; // Y eksenindeki minimum değer
        public float maxYPosition = 5f; // Y eksenindeki maksimum değer
        public float minScale = 1f; // Minimum ölçek değerleri
        public float maxScale = 2f; // Maksimum ölçek değerleri
        public LayerMask collisionMask; // Çakışma kontrolü için layer mask

        private List<GameObject> spawnedObjects = new List<GameObject>(); // Oluşturulan objeleri tutan liste

        void Start()
        {
            PlaceRandomObjects();
        }

        public void PlaceRandomObjects()
        {
            for (int i = 0; i < numberOfObjects; i++)
            {
                bool isValidPosition = false;
                Vector3 randomPosition = Vector3.zero;
                Vector3 randomScale = Vector3.one;

                // Rastgele bir prefab seç
                GameObject selectedPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

                // Rastgele X ve Z pozisyonları seç ve çakışma kontrolü yap
                while (!isValidPosition)
                {
                    float randomX = Random.Range(-areaSize.x / 2, areaSize.x / 2);
                    float randomZ = Random.Range(-areaSize.z / 2, areaSize.z / 2);
                    float randomY = Random.Range(minYPosition, maxYPosition);
                    randomPosition = new Vector3(randomX, randomY, randomZ) + transform.position;

                    // Çakışma kontrolü
                    if (!Physics.CheckSphere(randomPosition, 0.5f, collisionMask))
                    {
                        isValidPosition = true;
                    }
                }

                // Rastgele bir ölçek değeri seç
                float scale = Random.Range(minScale, maxScale);
                randomScale = Vector3.one * scale;

                // Seçilen prefabı geçerli pozisyonda ve ölçekte oluştur
                GameObject newObject = Instantiate(selectedPrefab, randomPosition, Quaternion.identity);
                newObject.transform.localScale = randomScale; // Ölçeği ayarla
                spawnedObjects.Add(newObject); // Oluşturulan objeyi listeye ekle

                // Yeni objeyi bu scriptin altına bağla
                newObject.transform.SetParent(transform);
            }
        }

        public void DeleteObjects()
        {
            foreach (GameObject obj in spawnedObjects)
            {
                DestroyImmediate(obj);
            }
            spawnedObjects.Clear();
        }
    }
}
