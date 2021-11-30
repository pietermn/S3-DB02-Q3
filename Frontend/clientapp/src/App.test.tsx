import MmPage from "./pages/machinemonitoring/MachineMonitoringPage";
import { shallow } from "enzyme";

describe("HomePage", () => {
    let wrapper = shallow(<MmPage />);

    it("should render table header", () => {
        expect(wrapper.find("tr").first().find("th").length).toBe(4);
    });
});
