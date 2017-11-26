
    import { Project } from './Project';
import { RepositoryType } from './RepositoryType';
import { ProjectTrackingTool } from './ProjectTrackingTool';
import { ReleaseNote } from './ReleaseNote';
import { EntityBase } from './EntityBase';
    export class Repository extends EntityBase<number> {
        
        name: string;
        url: string;
        owner: string;
        projectId: number;
        project: Project;
        repositoryType: RepositoryType;
        projectTrackingToolId: number;
        projectTrackingTool: ProjectTrackingTool;
        releaseNotes: ReleaseNote[];
        }

    