using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEMon.XmlGenerator
{
    internal class Todo
    {
        //! Rework to CCP's Static Data (sqlite->json or yaml).
        //! See https://developers.eveonline.com/docs/services/static-data/#schema-changes

        //! (Research) Agents in npcCharacters.json/yaml and agentTypes.json/yaml.
        //! Blueprints in blueprints.json/yaml.
        //! Celestials in 
        //  mapConstellations, 
        //  mapRegions, 
        //  mapSolarSystems, 
        //  mapStars, 
        //  mapStargates, 
        //  mapMoons, 
        //  mapAsteroidBelts
        // 


        //! a) json static dump -> xml converter tool.
        //! b) swap xml for json when reading.
        //!      See Staticblueprintss.Load()
        //!      See EVEMon.Common\Serialization\Datafiles\

        //Updated (2025):
        //eve-reprocessing-en-US.xml.gzip			eve-reprocessing-en-US.xml
        //eve-items-en-US.xml.gzip				eve-items-en-US.xml
        //eve-blueprints-en-US.xml.gzip			eve-blueprints-en-US.xml
        //eve-geography-en-US.xml.gzip			eve-geography-en-US.xml
        //eve-skills-en-US.xml.gzip				eve-skills-en-US.xml
        //eve-properties-en-US.xml.gzip			eve-properties-en-US.xml

        //Old (2021-2023):
        //eve-masteries-en-US.xml.gzip			eve-masteries-en-US.xml
        //eve-certificates-en-US.xml.gzip			eve-certificates-en-US.xml

        //blueprints.xml

        //<blueprint id="681" name="Clone Grade Beta Blueprint" icon="" metaGroup="T1" productTypeID="165" productionTime="600" researchProductivityTime="210" researchMaterialTime="210" researchCopyTime="480" reverseEngineeringTime="0" inventionTime="0" reactionTime="0" maxProductionLimit="300">
        //  <inventTypeIDs />
        //  <m id="38" quantity="86" activityId="1" />
        //</blueprint>

        //LET OP, geen comma's tussen objecten.

        //blueprints.jsonl
        //{"_key": 681, "activities": {"copying": {"time": 480}, "manufacturing": {"materials": [{"quantity": 86, "typeID": 38}], "products": [{"quantity": 1, "typeID": 165}], "time": 600}, "research_material": {"time": 210}, "research_time": {"time": 210}},

        //types.json

        //{"_key": 681, "basePrice": 9999999.0, "groupID": 104, "iconID": 34, "name": {"de": "Clone Grade Beta Blueprint", "en": "Clone Grade Beta Blueprint", "es": "Plano de clon de grado Beta", "fr": "Plan de construction Clone grade Bêta", "ja": "クローングレードベータブループリント", "ko": "클론 그레이드 베타 블루프린트", "ru": "Clone Grade Beta Blueprint", "zh": "B级克隆蓝图"}, "portionSize": 1, "published": false, "volume": 0.01}

        //! NOTE jsonl = Json Lines.

        //? JsonObject group = JsonSerializer.Deserialize<JsonObject>(line);
        //? group["_key"]
        //? group["name"]["en"]
    }
}
