using System.Runtime.ConstrainedExecution;
using System;
using UnityEngine;

public class MyLocation : MonoBehaviour
{
    private bool[] roomFlags = new bool[4];
    private Animator animator;
    private string currentRoom;

    // Prüft, welche Taste gedrückt wurde und ruft die entsprechende Funktion auf
    private void Update()
    {
        Vector3 cameraPosition = transform.position;

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            HandleButtonOne(cameraPosition);
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            HandleButtonTwo(cameraPosition);
        }
        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            HandleButtonX(cameraPosition);
        }
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            HandleButtonFour(cameraPosition);
        }
    }
    // Prüft, in welchem ​​Raum es sich befindet und aktiviert die entsprechende Animation
    private void HandleButtonOne(Vector3 cameraPosition)
    {
        if (TryHandleRoomAction(cameraPosition, "Room1", "Stegasaurus_20K", "Stegasaurus", 0)) return;
        if (TryHandleRoomAction(cameraPosition, "Room2", "Pachycephalasaurus_24K", "Pachy", 1)) return;
        if (TryHandleRoomAction(cameraPosition, "Room3", "Rampaging T-Rex", "TRex", 2)) return;
        if (TryHandleRoomAction(cameraPosition, "Room4", "Raptor_Animated_FBX_5K", "Raptor", 3)) return;

        currentRoom = "Unknown";
    }

    // Prüft, in welchem ​​Raum es sich befindet und aktiviert die entsprechende Audio
    private void HandleButtonTwo(Vector3 cameraPosition)
    {
        if (TryPlayAudioClip(cameraPosition, "Room1", "Stegasaurus_20K", "Stegasaurus/Audio")) return;
        if (TryPlayAudioClip(cameraPosition, "Room2", "Pachycephalasaurus_24K", "Pachy/Audio")) return;
        if (TryPlayAudioClip(cameraPosition, "Room3", "Rampaging T-Rex", "TRex/Audio")) return;
        if (TryPlayAudioClip(cameraPosition, "Room4", "Raptor_Animated_FBX_5K", "Raptor/Audio")) return;

        currentRoom = "Unknown";
    }

    // Prüft, in welchem ​​Raum es sich befindet, und stellt das Objekt in diesem Raum wieder an seinen ursprünglichen Standort und seine ursprüngliche Größe zurück
    public void HandleButtonX(Vector3 cameraPosition)
    {
        if (TryPositionObject(cameraPosition, "Room1", "Stegasaurus_20K", new Vector3(-5.0f, 0.5f, 4.8f), new Vector3(0, 180, 0), new Vector3(0.85f, 0.85f, 0.85f))) return;
        if (TryPositionObject(cameraPosition, "Room2", "Pachycephalasaurus_24K", new Vector3(5.0f, 0.496f, 5.0f), new Vector3(0, 180, 0), new Vector3(1.2f, 1.2f, 1.2f))) return;
        if (TryPositionObject(cameraPosition, "Room3", "Rampaging T-Rex", new Vector3(5.0f, 0.501f, -5.760f), new Vector3(0, 0, 0), new Vector3(0.6208f, 0.6208f, 0.6208f))) return;
        if (TryPositionObject(cameraPosition, "Room4", "Raptor_Animated_FBX_5K", new Vector3(-5.0f, 0.48f, -5.0f), new Vector3(0, 180, 0), new Vector3(1.0f, 1.0f, 1.0f))) return;

        currentRoom = "Unknown";
    }

    // Prüft, in welchem ​​Raum es sich befindet, und ändert das Material, das sich auf das Objekt in diesem Raum bezieht
    private void HandleButtonFour(Vector3 cameraPosition) 
    {
        if (TryChangeMaterial(cameraPosition, "Room1", "Stegasaurus_LOD", "Stegasaurus/material")) return;
        if (TryChangeMaterial(cameraPosition, "Room2", "Retopo_24K", "Pachy/material")) return;
        if (TryChangeMaterial(cameraPosition, "Room3", "TRex.2", "TRex/material")) return;
        if (TryChangeMaterial(cameraPosition, "Room4", "Retopo", "Raptor/material")) return;

        currentRoom = "Unknown";
    }

    // Eine Funktion, die prüft, ob sich der Benutzer im angegebenen Raum befindet und die Aktion ausführt
    private bool TryHandleRoomAction(Vector3 cameraPosition, string roomTag, string objectName, string animationPath, int roomIndex)
    {
        if (IsInRoom(cameraPosition, roomTag))
        {
            currentRoom = roomTag;
            GameObject obj = SelectRoomObject(objectName);
            ToggleAnimation(obj, animationPath, roomIndex);
            roomFlags[roomIndex] = !roomFlags[roomIndex];
            return true;
        }
        return false;
    }

    // Eine Funktion, die prüft, ob sich der Benutzer im angegebenen Raum befindet und den entsprechenden Ton abspielt
    private bool TryPlayAudioClip(Vector3 cameraPosition, string roomTag, string objectName, string audioPath)
    {
        if (IsInRoom(cameraPosition, roomTag))
        {
            currentRoom = roomTag;
            GameObject obj = SelectRoomObject(objectName);
            PlayAudioClip(obj, audioPath);
            return true;
        }
        return false;
    }

    // Eine Funktion, die prüft, ob sich der Benutzer im angegebenen Raum befindet und das Material des Objekts ändert
    private bool TryChangeMaterial(Vector3 cameraPosition, string roomTag, string objectName, string materialPath)
    {
        if (IsInRoom(cameraPosition, roomTag))
        {
            currentRoom = roomTag;
            GameObject obj = SelectRoomObject(objectName);
            ChangeMaterial(obj, materialPath);
            return true;
        }
        return false;
    }


    private bool TryPositionObject(Vector3 cameraPosition, string roomTag, string objectName, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        Debug.Log("test" + cameraPosition);
        if (IsInRoom(cameraPosition, roomTag))
        {
            currentRoom = roomTag;
            GameObject obj = SelectRoomObject(objectName);
            SetObjectTransform(obj, position, rotation, scale);
            return true;
        }
        return false;
    }

    // prüft, ob die Position des Spielers innerhalb eines bestimmten Raums liegt
    private bool IsInRoom(Vector3 position, string roomTag)
    {
        GameObject room = GameObject.FindGameObjectWithTag(roomTag);
        if (room != null)
        {
            BoxCollider roomCollider = room.GetComponent<BoxCollider>();
            return roomCollider != null && roomCollider.bounds.Contains(position);
        }
        return false;
    }

    // lädt das angegebene Objekt im Raum
    private GameObject SelectRoomObject(string objectName)
    {
        GameObject roomObject = GameObject.Find(objectName);
        if (roomObject == null)
        {
            Debug.LogWarning("Room object not found: " + objectName);
        }
        return roomObject;
    }

    //startet oder stoppt eine Animation für ein Objekt
    private void ToggleAnimation(GameObject obj, string animationPath, int roomIndex)
    {
        if (obj == null) return;

        animator = obj.GetComponent<Animator>();
        if (roomFlags[roomIndex])
        {
            animator.runtimeAnimatorController = null;
        }
        else
        {
            RuntimeAnimatorController[] controller = Resources.LoadAll<RuntimeAnimatorController>(animationPath);
            int randomIndex = UnityEngine.Random.Range(0, controller.Length);
            animator.runtimeAnimatorController = controller[randomIndex];
        }
    }

    // spielt einen Audioclip für ein Objekt ab
    private void PlayAudioClip(GameObject obj, string audioPath)
    {
        if (obj == null) return;

        AudioSource audioSource = obj.GetComponent<AudioSource>();
        AudioClip[] clips = Resources.LoadAll<AudioClip>(audioPath);
        int randomIndex = UnityEngine.Random.Range(0, clips.Length);
        Debug.Log(audioPath[randomIndex]);
        if (clips != null)
        {
            audioSource.clip = clips[randomIndex];
            audioSource.Play();
        }
    }

    //ändert das Material eines Objekts
    public void ChangeMaterial(GameObject obj, string materialPath)
    {
        Material[] materials = Resources.LoadAll<Material>(materialPath);
        Renderer objectRenderer = obj.GetComponent<Renderer>();
        if (materials.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, materials.Length);
            objectRenderer.material = materials[randomIndex];
        }
    }

    // setzt Position, Rotation und Skalierung eines Objekts
    private void SetObjectTransform(GameObject obj, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        if (obj == null) return;

        obj.transform.position = position;
        obj.transform.rotation = Quaternion.Euler(rotation);
        obj.transform.localScale = scale;
    }
}
