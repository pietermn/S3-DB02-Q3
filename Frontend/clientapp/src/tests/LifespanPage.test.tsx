import { shallow } from "enzyme";
import LsPage from "../pages/lifespan/LifespanPage";

describe("LsPage", () => {
    let wrapper = shallow(<LsPage />);

    it("should render 4 headers", () => {
        expect(wrapper.find("thead").first().find("tr").first().find("th").length).toBe(4);
    });
});
