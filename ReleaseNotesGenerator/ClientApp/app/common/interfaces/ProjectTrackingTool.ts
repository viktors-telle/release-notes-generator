
    import { Repository } from './Repository';
import { ProjectTrackingToolType } from './ProjectTrackingToolType';
import { EntityBase } from './EntityBase';
    export class ProjectTrackingTool extends EntityBase<number> {
        
        name: string
        url: string
        projectName: string
        type: ProjectTrackingToolType
        repositories: Repository[]
        }

    