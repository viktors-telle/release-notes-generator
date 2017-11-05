
    import { ProjectTrackingToolType } from './ProjectTrackingToolType';
import { Repository } from './Repository';
import { EntityBase } from './EntityBase';
    export class ProjectTrackingTool extends EntityBase<number> {
        
        name: string;
        url: string;
        projectName: string;
        type: ProjectTrackingToolType;
        repositories: Repository[];
        }

    