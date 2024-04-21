using System.Collections;
using System.Collections.Generic;

public class EventFactory
{
    public ClickBornEvent GetClickBorn() { return new ClickBornEvent(); }

    public ClickGoEvent GetClickGo() { return new ClickGoEvent(); }

    public ClickSpawnedRabbitEvent GetClickSpawnedRabbit() { return new ClickSpawnedRabbitEvent();}

    public ClickUIRabbitEvent GetClickUIRabbit() { return new ClickUIRabbitEvent(); }

    public InitLevelEvent GetInitLevel() { return new InitLevelEvent();}

    public EndLevelEvent GetEndLevel() { return new EndLevelEvent();}
}
