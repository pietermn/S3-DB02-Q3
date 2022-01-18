import React from "react";
import * as d3 from "d3";

export const useD3 = (renderChartFn: any, dependencies: any) => {
    const ref = React.useRef();

    React.useEffect(() => {
        renderChartFn(d3.select(ref.current || null));
        return () => {};
        // eslint-disable-next-line
    }, dependencies);
    return ref;
};
