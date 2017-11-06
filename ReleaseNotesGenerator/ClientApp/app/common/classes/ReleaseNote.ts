
    import { Repository } from './Repository';
import { EntityBase } from './EntityBase';
    export class ReleaseNote extends EntityBase<number> {
        
        notes: string;
        created: Date;
        repositoryId: number;
        repository: Repository;
        }

    