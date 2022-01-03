using System;

using siwe.Cacao;

namespace siwe.Anchor
{
    public interface IAnchorProvider
    {
        AnchorProviderType GetType();

        string Anchor(CacaoMessage msg);
    }
}
