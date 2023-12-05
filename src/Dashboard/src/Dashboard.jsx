import './Dashboard.css'
import CraneVisualisation from "./CraneVisualisation/CraneVisualisation.jsx";
import DataTable from "./DataTable/DataTable.jsx";
import AnimatedGraphs from "./AnimatedGraphs/AnimatedGraphs.jsx";
import EmergencyButton from "./EmergencyButton/EmergencyButton.jsx";
import InputVisualisation from "./InputVisualisation/InputVisualisation.jsx";

export default function Dashboard() {

  return (
    <div id={"container"}>
      <CraneVisualisation />
      <DataTable />
      <AnimatedGraphs />
      <EmergencyButton />
      <InputVisualisation />
    </div>
  )
}
