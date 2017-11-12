
    import { Repository } from './Repository';
import { EntityBase } from './EntityBase';
    export class ReleaseNote extends EntityBase<number> {
        
        notes: string;
        created: Date;
        repositoryId: number;
        repository: Repository;
        repositoryPath: string;
        branchName: string;
        from: Date;
        until: Date;
        }

    