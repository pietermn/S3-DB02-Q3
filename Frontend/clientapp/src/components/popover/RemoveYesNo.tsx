import { Popover, Button } from "@mui/material";
import { Close as CloseIcon, Check as DeleteIcon } from "@mui/icons-material";
import { useTranslation } from "react-i18next";

interface IRemovePopover {
    openState: boolean;
    setOpenState: (state: string) => void;
    anchorId: string;
    anchor: SVGElement | null;
    setAnchor: (anchor: SVGElement | null) => void;
    message: React.ReactNode;
    remove: Function;
    cancel: Function;
}

export default function RemoveYesNo(props: IRemovePopover) {
    const { t } = useTranslation();

    function close() {
        props.setAnchor(null);
        props.setOpenState("");
    }

    return (
        <Popover
            className="Popover__Remove__User"
            id={props.anchorId}
            open={props.openState}
            anchorEl={props.anchor}
            onClose={() => close()}
            anchorOrigin={{
                vertical: "bottom",
                horizontal: "right",
            }}
            transformOrigin={{
                vertical: "top",
                horizontal: "right",
            }}
        >
            <div className="Popover__Yes__No">
                <div>{props.message}</div>
                <div>
                    <Button
                        onClick={() => {
                            props.remove();
                            close();
                        }}
                        variant="contained"
                        className="Yes"
                        startIcon={<DeleteIcon />}
                    >
                        {t("yes.label")}
                    </Button>
                    <Button
                        onClick={() => {
                            close();
                            props.cancel();
                        }}
                        variant="contained"
                        className="No"
                        startIcon={<CloseIcon />}
                    >
                        {t("no.label")}
                    </Button>
                </div>
            </div>
        </Popover>
    );
}
