
    import { Project } from './Project';
import { ProjectTrackingTool } from './ProjectTrackingTool';
import { Branch } from './Branch';
import { RepositoryType } from './RepositoryType';
import { EntityBase } from './EntityBase';
    export class Repository extends EntityBase<number> {
        
        name: string
        url: string
        projectId: number
        project: Project
        repositoryType: RepositoryType
        projectTrackingToolId: number
        projectTrackingTool: ProjectTrackingTool
        branches: Branch[]
        }

    