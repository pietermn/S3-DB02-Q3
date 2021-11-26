import { shallow } from "enzyme";
import MmPage from "../pages/machinemonitoring/MachineMonitoringPage";

describe("MmPage", () => {
    let wrapper = shallow(<MmPage />);

    it("should render 4 headers", () => {
        expect(wrapper.find("thead").first().find("tr").first().find("th").length).toBe(4);
    });
});
