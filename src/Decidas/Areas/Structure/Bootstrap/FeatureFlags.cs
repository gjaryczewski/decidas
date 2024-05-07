namespace Decidas.Areas.Structure.Bootstrap;

public record StructureFeatureFlags(
    bool AllowIncomingWebhooks,
    bool AllowOutgoingWebhooks
);
