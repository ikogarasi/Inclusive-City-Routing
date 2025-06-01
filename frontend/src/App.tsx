import styles from "./App.module.less";
import { Route, Routes } from "react-router-dom";
import "leaflet/dist/leaflet.css";
import "./App.css";
import { InfoPage } from "./pages/InfoPage/InfoPage";
import { MapPage } from "./pages/MapPage/MapPage";

function App() {
  return (
    <div className={styles.main}>
      <Routes>
        <Route path="/" element={<MapPage />} />
        <Route path="/info/:type/:structureId" element={<InfoPage />} />
      </Routes>
    </div>
  );
}

export default App;
