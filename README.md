# Augmented-Reality-based-Surgery

Augmented Reality (AR) application developed in **Unity 3D** for real-time **bone tracking** and **3D model registration**.
The application overlays a **virtual bone model** onto a real-world bone (or bone model) with precise alignment and motion tracking using **fiducial markers**.

---

## 🚀 Features

* **Marker Detection**

  * Detects and tracks **ArUco markers** or **NDI Infrared markers**.
  * Real-time 3D position calculation of markers relative to the camera.

* **AR Model Registration**

  * Collects 3D surface points of the physical bone using a marker-attached pen.
  * Aligns a pre-created virtual 3D bone model with the captured point cloud using a registration algorithm.

* **Real-Time Model Tracking**

  * Registered virtual model moves in sync with the real bone when the physical object is moved.

* **Unity3D Integration**

  * Renders the live camera feed as background in Unity.
  * Displays interactive 3D models (.stl format).
  * Simple UI controls for model visibility and orthographic views.

---

## 📸 Project Workflow

### **Phase 1 – Marker Detection**

1. Attach **Marker-1** to the physical bone model.
2. Detect marker position using the camera feed (RGB or IR).
3. Render a cube or 3D placeholder at the marker’s position in Unity.
4. Ensure cube moves dynamically with marker motion.

### **Phase 2 – Registration**

1. Attach **Marker-2** to a pen tool.
2. Collect \~40 surface points on the bone by touching with the pen tip.
3. Compute 3D positions relative to **Marker-1**.
4. Align the pre-scanned virtual bone model with collected points using a registration algorithm.
5. Overlay aligned model on live AR view so it tracks with the real bone.

---

## 📜 License

This project is released under the **MIT License**.

