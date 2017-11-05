
    import { Repository } from './Repository';
import { RepositoryItemPath } from './RepositoryItemPath';
import { EntityBase } from './EntityBase';
    export class Branch extends EntityBase<number> {
        
        name: string;
        lastCommitId: string;
        lastCommitDateTime: Date;
        repositoryId: number;
        repository: Repository;
        repositoryItemPaths: RepositoryItemPath[];
        }

    