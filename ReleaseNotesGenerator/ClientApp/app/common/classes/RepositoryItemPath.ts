
    import { Branch } from './Branch';
import { EntityBase } from './EntityBase';
    export class RepositoryItemPath extends EntityBase<number> {
        
        path: string;
        branchId: number;
        lastCommitId: string;
        lastCommitDateTime: Date;
        branch: Branch;
        }

    