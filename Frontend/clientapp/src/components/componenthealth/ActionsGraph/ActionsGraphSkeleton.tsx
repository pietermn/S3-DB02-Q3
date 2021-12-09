import { Skeleton } from "@mui/material";

interface IActionsGraphSkeleton {
    isLoading: boolean;
}

export default function ActionsGraphSkeleton({ isLoading }: IActionsGraphSkeleton) {
    return (
        <div className="ActionsGraph-Skeleton">
            <Skeleton className="Axis" variant="rectangular" />
            <Skeleton className="Axis" variant="rectangular" />
            <Skeleton className="SkeletonBar" variant="rectangular" />
            <Skeleton className="SkeletonBar" variant="rectangular" />
            <Skeleton className="SkeletonBar" variant="rectangular" />
            <Skeleton className="SkeletonBar" variant="rectangular" />
        </div>
    );
}
