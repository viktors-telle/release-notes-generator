export interface Project extends EntityBase<number> {
    Name: string;
    ApiKey: string;
    IsDeactivated: boolean;
    Repositories: Repository[];
}

export interface Repository extends EntityBase<number> {    
    Name: string;
    Url: string;    
    ProjectId: number;
    Project: Project;    
    RepositoryType: RepositoryType;    
    ProjectTrackingToolId: number;
    ProjectTrackingTool: ProjectTrackingTool;
    Branches: Branch[];
}

export interface EntityBase<T> {
    Id: T;
}

export interface Branch extends EntityBase<number> {    
    Name: string;
    LastCommitId: string;
    LastCommitDateTime: Date;    
    RepositoryId: number;
    Repository: Repository;
    RepositoryItemPaths: RepositoryItemPath[];
}


export interface ProjectTrackingTool extends EntityBase<number> {    
    Name: string;    
    Url: string;    
    ProjectName: string;    
    Type: ProjectTrackingToolType;
    Repositories: Repository[];
}

export interface RepositoryItemPath extends EntityBase<number> {    
    Path: string;    
    BranchId: number;
    LastCommitId: string;
    Branch: Branch;
}

export enum ProjectTrackingToolType {
    Tfs = 1,
    Jira = 2
}

export enum RepositoryType {
    Git = 1,
    Tfs = 2
}