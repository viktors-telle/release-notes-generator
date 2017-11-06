﻿
    import { Project } from './Project';
import { RepositoryType } from './RepositoryType';
import { ProjectTrackingTool } from './ProjectTrackingTool';
import { Branch } from './Branch';
import { ReleaseNote } from './ReleaseNote';
import { EntityBase } from './EntityBase';
    export class Repository extends EntityBase<number> {
        
        name: string;
        url: string;
        projectId: number;
        project: Project;
        repositoryType: RepositoryType;
        projectTrackingToolId: number;
        projectTrackingTool: ProjectTrackingTool;
        branches: Branch[];
        releaseNotes: ReleaseNote[];
        }

    