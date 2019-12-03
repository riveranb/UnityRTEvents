# UnityRTEvents
test (mouse) events in rendering to RenderTexture
- https://riveragamer.blogspot.com/2019/12/unity-render-to-rendertexture.html
- https://riveragamer.blogspot.com/2019/12/render-to-rendertexture.html
- https://forum.unity.com/threads/physicsraycaster-used-with-render-to-rendertexture.787502/


## TestMain.unity: 1 Camera, everything works just fine.

## TestCamera2.unity: 2 Camera, events missed/broke
SceneCamera: "MainCamera", render cube to RenderTexture; using PhysicsRaycaster.
ScreenCamera: only blits RenderTexture onto screen.
### Issue 
When RenderTexture is different sized with real screen, PhysicsRaycaster/EventSystem failed with incorrect cursor position.
