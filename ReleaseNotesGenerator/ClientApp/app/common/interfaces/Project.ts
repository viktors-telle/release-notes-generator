
    import { Repository } from './Repository';
import { EntityBase } from './EntityBase';
    export class Project extends EntityBase<number> {
        
        name: string
        apiKey: string
        isDeactivated: boolean
        repositories: Repository[]
        }

    