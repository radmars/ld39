using UnityEngine;
using System.Collections;
public class Crosshair : MonoBehaviour
{
    Texture2D m_CrosshairTex;
    Vector2 m_WindowSize;    //More like "last known window size".
    Rect m_CrosshairRect;

    public float crosshairScale = 1;
    public Texture2D crosshairTexture;
    void Start()
    {
        m_CrosshairTex = new Texture2D(2, 2);
        m_WindowSize = new Vector2(Screen.width, Screen.height);
        CalculateRect();
    }

    void Update()
    {
        if (m_WindowSize.x != Screen.width || m_WindowSize.y != Screen.height)
        {
            CalculateRect();
        }
    }

    void CalculateRect()
    {
        m_CrosshairRect = new Rect((m_WindowSize.x - m_CrosshairTex.width) / 2.0f,
                                    (m_WindowSize.y - m_CrosshairTex.height) / 2.0f,
                                    m_CrosshairTex.width, m_CrosshairTex.height);
    }

   void OnGUI()
     {
         //if not paused
         if(Time.timeScale != 0)
         {
             if(crosshairTexture!=null)
                         GUI.DrawTexture(new Rect((Screen.width-crosshairTexture.width*crosshairScale)/2 ,(Screen.height-crosshairTexture.height*crosshairScale)/2, crosshairTexture.width*crosshairScale, crosshairTexture.height*crosshairScale),crosshairTexture);
             else
               {
                    GUI.DrawTexture(new Rect((Screen.width-m_CrosshairTex.width*crosshairScale)/2 ,(Screen.height-m_CrosshairTex.height*crosshairScale)/2, m_CrosshairTex.width*crosshairScale, m_CrosshairTex.height*crosshairScale),m_CrosshairTex);
               }
         }
     }
}